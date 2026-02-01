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
    }
}