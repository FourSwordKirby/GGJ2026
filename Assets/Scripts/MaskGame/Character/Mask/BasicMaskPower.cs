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
    }
}