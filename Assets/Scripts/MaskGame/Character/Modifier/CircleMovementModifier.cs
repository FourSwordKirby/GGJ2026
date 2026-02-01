using System;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    public class CircleMovementModifier : MovementModifier
    {
        public float CircleRadius = 1;
        public float MaxSpeed = 5;
        public float Acceleration = 200;

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            Vector3 pivotDirection = -character.ExtendedRigidbody.transform.right;
            bool hasInput = inputs.MovementIntention.magnitude > .2;
            if (hasInput)
            {
                pivotDirection *= -1;
            }
            Vector3 charToCirclePivot = pivotDirection * CircleRadius;
            Vector3 circlePivot = character.ExtendedRigidbody.Position + charToCirclePivot;
            float travelDistance = MaxSpeed * deltaTime;
            float degreesTraveled = travelDistance / (2.0f * (float)Math.PI * CircleRadius) * 360.0f;
            if (!hasInput)
            {
                degreesTraveled *= -1;
            }
            Quaternion rot = Quaternion.Euler(0.0f, degreesTraveled, 0.0f);
            Vector3 pivotToNewLoc = rot * (-charToCirclePivot);
            Vector3 newLoc = circlePivot + pivotToNewLoc;
            Vector3 charToNewLoc = (newLoc - character.ExtendedRigidbody.Position).normalized;
            
            Vector3 impulse = ComputeMovementImpulse(character, charToNewLoc, deltaTime);
            character.ExtendedRigidbody.ApplyImpulse(impulse, true);
            character.SetFacingDirection(charToNewLoc);
        }

        protected virtual Vector3 ComputeMovementImpulse(MaskGameCharacter character, Vector3 intentedMove, float deltaTime)
        {
            Vector3 xzInputDirection = intentedMove;
            xzInputDirection.y = 0.0f;

            Vector3 targetVelocity = xzInputDirection * MaxSpeed;
            Vector3 impulse = Vector3.zero;
            float acceleration = Acceleration;

            impulse = targetVelocity - character.ExtendedRigidbody.Velocity;
            impulse = Vector3.ProjectOnPlane(impulse, Vector3.up);

            // Clamp the max impulse so that it doesn't exceed our flying acceleration.
            float maxImpulseMagnitude = acceleration * deltaTime;
            Vector3 clampedImpulse = Vector3.ClampMagnitude(impulse, maxImpulseMagnitude);

            return clampedImpulse;
        }
    }
}