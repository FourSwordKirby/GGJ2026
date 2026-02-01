using Core;
using System;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    [Serializable]
    public class ReversedWalkingMovementModifier : WalkingMovementModifier
    {
        protected override Vector3 ComputeWalkingMovementImpulse(MaskGameCharacter character, Vector3 intentedMove, float deltaTime)
        {
            return base.ComputeWalkingMovementImpulse(character, -intentedMove, deltaTime);
        }

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            base.ApplyInputToCharacter(character, inputs, deltaTime);

            Vector3 facing = Vector3.ProjectOnPlane(character.ExtendedRigidbody.Velocity, Vector3.up);
            character.SetFacingDirection(-facing);
        }
    }
}