using System;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    public class CircleMovementModifier : MovementModifier
    {
        public float CircleRadius = 1;
        public float MaxSpeed = 5;
        public float Acceleration = 200;
        public float DegreesPerSecond = 180;

        public Vector3 LastFacingDir;
        public float DegreesToTravel;

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            float rotSpeed = DegreesPerSecond;
            bool hasInput = inputs.MovementIntention.magnitude > .2;
            if (hasInput)
            {
                rotSpeed *= -1;
            }
            Quaternion rot = Quaternion.Euler(0.0f, rotSpeed * deltaTime, 0.0f);
            Vector3 newFacingDirection = rot * LastFacingDir;
            character.SetFacingDirection(newFacingDirection);
            Vector3 impulse = ComputeMovementImpulse(character, newFacingDirection, deltaTime);
            character.ExtendedRigidbody.ApplyImpulse(impulse, true);
            LastFacingDir = newFacingDirection;

            //bool doubleRotation = false;
            //// Fix for stuck at wall
            //float rotationChangeSinceLastFrame = Vector3.Angle(LastFacingDir, character.GetFacingDirection());
            //if (rotationChangeSinceLastFrame < Math.Abs(DegreesToTravel) * .2)
            //{
            //    character.SetFacingDirection(LastFacingDir);
            //    doubleRotation = true;
            //}
            //LastFacingDir = character.GetFacingDirection();

            //Vector3 pivotDirection = -Vector3.Cross(Vector3.up, LastFacingDir);
            //Vector3 charToCirclePivot = pivotDirection * CircleRadius;
            //Vector3 circlePivot = character.ExtendedRigidbody.Position + charToCirclePivot;
            //float travelDistance = MaxSpeed * deltaTime;
            //float degreesTraveled = travelDistance / (2.0f * (float)Math.PI * CircleRadius) * 360.0f;
            //if (!hasInput)
            //{
            //    degreesTraveled *= -1;
            //}
            //if (doubleRotation)
            //{
            //    degreesTraveled *= 8;
            //}
            //DegreesToTravel = degreesTraveled;
            //Quaternion rot = Quaternion.Euler(0.0f, degreesTraveled, 0.0f);
            //Vector3 pivotToNewLoc = rot * (-charToCirclePivot);
            //Vector3 newLoc = circlePivot + pivotToNewLoc;
            //Vector3 charToNewLoc = (newLoc - character.ExtendedRigidbody.Position).normalized;

            //Vector3 impulse = ComputeMovementImpulse(character, charToNewLoc, deltaTime);
            //character.ExtendedRigidbody.ApplyImpulse(impulse, true);
            //character.SetFacingDirection(charToNewLoc);
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