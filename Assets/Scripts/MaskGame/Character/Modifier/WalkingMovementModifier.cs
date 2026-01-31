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

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            Vector3 impulse = ComputeWalkingMovementImpulse(character, inputs.MovementIntention, deltaTime);
            character.ExtendedRigidbody.ApplyImpulse(impulse, true);
        }

        protected Vector3 ComputeWalkingMovementImpulse(MaskGameCharacter character, Vector3 intentedMove, float deltaTime)
        {
            Vector3 xzInputDirection = intentedMove;
            xzInputDirection.y = 0.0f;

            Vector3 targetVelocity = xzInputDirection * MaxWalkingSpeed;
            Vector3 impulse = Vector3.zero;
            float acceleration = WalkingAcceleration;

            impulse = targetVelocity - character.ExtendedRigidbody.Velocity;

            // Clamp the max impulse so that it doesn't exceed our flying acceleration.
            float maxImpulseMagnitude = acceleration * deltaTime;
            Vector3 clampedImpulse = Vector3.ClampMagnitude(impulse, maxImpulseMagnitude);

            return clampedImpulse;
        }
    }
}