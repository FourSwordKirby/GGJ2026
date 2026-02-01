using MaskGame.Character.Modifier;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class JockMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.JOCK;

        public JockMaskPower()
        {
            Movement = new CircleMovementModifier();
        }
    }
}