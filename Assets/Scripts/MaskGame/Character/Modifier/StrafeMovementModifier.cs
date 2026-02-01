using MaskGame.Character.Modifier;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    public class StrafeMovementModifier : WalkingMovementModifier
    {
        public float ElapsedTime = 0.0f;
        public float SwitchTime = 1.0f;
        public bool IsRight = true;

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            ElapsedTime += deltaTime;
            while (ElapsedTime > SwitchTime)
            {
                ElapsedTime -= SwitchTime;
                IsRight = !IsRight;
            }

            if (inputs.MovementIntention.magnitude > 0.2f)
            {
                character.SetFacingDirection(inputs.MovementIntention);
            }

            Vector3 rightOfFacing = Vector3.Cross(Vector3.up, character.GetFacingDirection());
            if (!IsRight)
            {
                rightOfFacing = -rightOfFacing;
            }

            if (character is PlayerCharacter playerCharacter)
            {
                playerCharacter.Animator.SetBool("theaterLeft", !IsRight);
            }

            Vector3 forcedIntention = inputs.MovementIntention + rightOfFacing;
            forcedIntention.Normalize();
            Vector3 impulse = ComputeWalkingMovementImpulse(character, forcedIntention, deltaTime);
            character.ExtendedRigidbody.ApplyImpulse(impulse, true);
        }
    }
}