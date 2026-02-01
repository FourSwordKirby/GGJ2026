using UnityEngine;
using MaskGame.Character.Modifier;
using System;

namespace MaskGame.Character.Mask
{
    [Serializable]
    public class BasicMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.BASIC;

        public BasicMaskPower()
        {
            Movement = new WalkingMovementModifier();
        }

        public override void Step(PlayerCharacter character, float deltaTime)
        {
            base.Step(character, deltaTime);

            const float SPEED_SLACK = 0.1f;
            float speed = character.ExtendedRigidbody.Velocity.magnitude;
            bool isMoving = Mathf.Abs(speed) > SPEED_SLACK;
            character.Animator.SetBool("basicMoving", isMoving);
        }
    }
}