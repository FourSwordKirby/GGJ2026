using Core;
using System;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    [Serializable]
    public class WalkingMovementModifier : MovementModifier
    {
        public float WalkingAcceleration { get; set; } = 100.0F;
        public float MaxWalkingSpeed { get; set; } = 5.0F;

        public WalkingMovementModifier(MaskGameCharacter character) : base(character)
        {
        }

        public override void ApplyInputToCharacter(CharacterInputs inputs, float deltaTime)
        {
            Vector3 impulse = ComputeWalkingMovementImpulse(inputs.MovementIntention, deltaTime);
            Character.ExtendedRigidbody.ApplyImpulse(impulse, true);
        }

        protected Vector3 ComputeWalkingMovementImpulse(Vector3 intentedMove, float deltaTime)
        {
            Vector3 xzInputDirection = intentedMove;
            xzInputDirection.y = 0.0f;

            Vector3 targetVelocity = xzInputDirection * MaxWalkingSpeed;
            Vector3 impulse = Vector3.zero;
            float acceleration = WalkingAcceleration;

            impulse = targetVelocity - Character.ExtendedRigidbody.Velocity;

            // Clamp the max impulse so that it doesn't exceed our flying acceleration.
            float maxImpulseMagnitude = acceleration * deltaTime;
            Vector3 clampedImpulse = Vector3.ClampMagnitude(impulse, maxImpulseMagnitude);

            return clampedImpulse;
        }
    }
}