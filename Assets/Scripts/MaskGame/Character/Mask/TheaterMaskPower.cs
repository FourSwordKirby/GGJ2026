using MaskGame.Character.Modifier;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class TheaterMaskPower : MaskPower
    {
        public override MaskState MaskState =>  MaskState.THEATER;

        public TheaterMaskPower()
        {
            Movement = new StrafeMovementModifier();
        }
    }
}
