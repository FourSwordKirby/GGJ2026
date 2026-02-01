using UnityEngine;
using MaskGame.Character.Modifier;

namespace MaskGame.Character.Mask
{
    public class NerdMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.NERD;

        public NerdMaskPower()
        {
            Movement = new TankMovementModifier();
        }

        public override void OnEnterPower(PlayerCharacter character)
        {
            base.OnEnterPower(character);
        }

        public override void Step(PlayerCharacter character, float deltaTime)
        {
            base.Step(character, deltaTime);

            // Moving Check

            const float SPEED_SLACK = 0.1f;
            float speed = character.ExtendedRigidbody.Velocity.magnitude;
            bool isMoving = Mathf.Abs(speed) > SPEED_SLACK;
            character.Animator.SetBool("nerdMoving", isMoving);
        }
    }
}