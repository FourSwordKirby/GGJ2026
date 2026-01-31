using MaskGame.Character.Modifier;

namespace MaskGame.Character.Mask
{
    public class BusinessMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.BUSINESS;

        public BusinessMaskPower()
        {
            Movement = new ReversedWalkingMovementModifier();
        }
    }
}