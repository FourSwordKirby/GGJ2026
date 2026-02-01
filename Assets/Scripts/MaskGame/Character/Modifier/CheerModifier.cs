using UnityEngine;

namespace MaskGame.Character.Modifier
{
    public class CheerModifier : MovementModifier
    {
        public float MinorHopVerticalImpulse = 3;
        public float MinorHopHorizontalImpulse = 4;

        public float MajorHopVerticalImpulse = 3;
        public float MajorHopHorizontalImpulse = 4; 

        public bool DoMajorJump = false;
        public bool MajorJumpIsLeft = false;

        public bool DoMinorJump = false;

        public override void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {
            character.SetFacingDirection(inputs.MovementIntention);

            if (DoMajorJump)
            {
                Vector3 horizonalHopDirection = character.ExtendedRigidbody.transform.right;
                if (MajorJumpIsLeft)
                {
                    horizonalHopDirection = -horizonalHopDirection;
                }
                Vector3 impulse = horizonalHopDirection * MajorHopHorizontalImpulse + Vector3.up * MajorHopVerticalImpulse;
                character.ExtendedRigidbody.ApplyImpulse(impulse, true);
                DoMajorJump = false;
            }

            if (DoMinorJump)
            {
                Vector3 impulse = inputs.MovementIntention * MinorHopHorizontalImpulse +Vector3.up * MinorHopVerticalImpulse;
                character.ExtendedRigidbody.ApplyImpulse(impulse, true);
                DoMinorJump = false;
            }
        }

        public void TriggerMajorBeat(bool isLeft)
        {
            DoMajorJump = true;
            MajorJumpIsLeft = isLeft;
        }

        public void TriggerMinorBeat()
        {
            DoMinorJump = true;
        }
    }
}