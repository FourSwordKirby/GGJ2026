using MaskGame.Character.Modifier;
using MaskGame.Cheerleader;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class CheerleaderMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.CHEER;

        protected CheerleaderManager CheerManager;
        protected CheerModifier CheerModifier;
        protected CheerBeatCallbackInput QueuedInput;

        public CheerleaderMaskPower()
        {
            Movement = CheerModifier = new CheerModifier();
        }

        public override void OnEnterPower(PlayerCharacter character)
        {
            base.OnEnterPower(character);

            if (CheerManager == null)
            {
                CheerManager = GameObject.FindAnyObjectByType<CheerleaderManager>();
            }

            if (CheerManager != null)
            {
                CheerManager.RegisterBeatCallback(character, HandleBeatCallback);
            }
        }

        public override void OnExitPower(PlayerCharacter character)
        {
            CheerManager.RemoveBeatCallback(character);
            base.OnExitPower(character);
        }

        public override void Step(PlayerCharacter character, float deltaTime)
        {
            base.Step(character, deltaTime);

            // TODO (imonh) Add a ground layer mask and do a raycast instead

            bool isGrounded = Mathf.Abs(character.ExtendedRigidbody.Velocity.y) < 0.01f;

            character.Animator.SetBool("cheerJump", !isGrounded);
        }

        protected void HandleBeatCallback(CheerBeatCallbackInput input)
        {
            if (input.IsMinorBeat)
            {
                CheerModifier.TriggerMinorBeat();
            }
            else if (input.IsMajorBeat)
            {
                CheerModifier.TriggerMajorBeat(input.IsLeft);
            }
        }
    }
}