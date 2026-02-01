using MaskGame.Character.Modifier;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    public class JockMaskPower : MaskPower
    {
        public override MaskState MaskState => MaskState.JOCK;
        public CircleMovementModifier CircleMod;

        public JockMaskPower()
        {
            Movement = CircleMod = new CircleMovementModifier();
        }

        public override void OnEnterPower(PlayerCharacter character)
        {
            CircleMod.LastFacingDir = character.GetFacingDirection();
        }
    }
}