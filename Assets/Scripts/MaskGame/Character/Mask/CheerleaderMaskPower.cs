using MaskGame.Character.Modifier;
using MaskGame.Cheerleader;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class CheerleaderMaskPower : MaskPower
    {
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