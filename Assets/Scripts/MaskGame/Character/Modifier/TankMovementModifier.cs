using UnityEngine;

namespace MaskGame.Character.Modifier
{
    public class TankMovementModifier : MovementModifier
    {
        public float TurnSpeedDegreesPerSecond = 180;
        public float TankAcceleration = 50;
        public float TankMaxSpeed = 10;

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            character.ExtendedRigidbody.SetAngularVelocity(Vector3.zero);
            Vector3 facing = character.GetFacingDirection();
            float horizontal = inputs.MovementIntention.x;
            bool turning = false;

            if (Mathf.Abs(horizontal) > 0.1)
            {
                turning = true;
                float degrees = horizontal * TurnSpeedDegreesPerSecond * deltaTime;
                Quaternion rot = Quaternion.Euler(0, degrees, 0);
                facing = rot * facing;
                character.SetFacingDirection(facing);
            }

            if (character is PlayerCharacter playerCharacter)
            {
                playerCharacter.Animator.SetBool("nerdRotating", turning);
                playerCharacter.Animator.SetBool("nerdRotatingLeft", horizontal < 0.0f);
            }

            Vector3 intendedMove = inputs.MovementIntention.z * facing;
            Vector3 impulse = ComputeWalkingMovementImpulse(character, intendedMove, deltaTime);
            character.ExtendedRigidbody.ApplyImpulse(impulse, true);
        }
        protected virtual Vector3 ComputeWalkingMovementImpulse(MaskGameCharacter character, Vector3 intentedMove, float deltaTime)
        {
            Vector3 xzInputDirection = intentedMove;
            xzInputDirection.y = 0.0f;

            Vector3 targetVelocity = xzInputDirection * TankMaxSpeed;
            Vector3 impulse = Vector3.zero;
            float acceleration = TankAcceleration;

            impulse = targetVelocity - character.ExtendedRigidbody.Velocity;
            impulse = Vector3.ProjectOnPlane(impulse, Vector3.up);

            // Clamp the max impulse so that it doesn't exceed our flying acceleration.
            float maxImpulseMagnitude = acceleration * deltaTime;
            Vector3 clampedImpulse = Vector3.ClampMagnitude(impulse, maxImpulseMagnitude);

            return clampedImpulse;
        }
    }
}