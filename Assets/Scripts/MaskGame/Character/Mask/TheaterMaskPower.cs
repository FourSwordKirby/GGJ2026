using MaskGame.Character.Modifier;
using MaskGame.Theater;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class TheaterMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.THEATER;

        protected TheaterManager TheaterManager;
        protected StrafeMovementModifier StrafeModifier;

        public TheaterMaskPower()
        {
            Movement = StrafeModifier = new StrafeMovementModifier();
        }

        public override void OnEnterPower(PlayerCharacter character)
        {
            base.OnEnterPower(character);

            if (TheaterManager == null)
            {
                TheaterManager = GameObject.FindAnyObjectByType<TheaterManager>();
            }

            if (TheaterManager != null)
            {
                TheaterManager.RegisterCallback(character, HandleCallback);
            }
        }

        public override void OnExitPower(PlayerCharacter character)
        {
            TheaterManager.RemoveCallback(character);
            base.OnExitPower(character);
        }

        void HandleCallback(TheaterManager.CallbackInput input)
        {
            StrafeModifier.IsRight = !input.IsLeft;
        }
    }
}
