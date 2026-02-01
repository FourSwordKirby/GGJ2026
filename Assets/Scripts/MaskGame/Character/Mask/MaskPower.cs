using MaskGame.Character.Modifier;
using System;
using UnityEngine;

namespace MaskGame.Character.Mask
{
    [Serializable]
    public class MaskPower
    {
        public virtual MaskState MaskState => MaskState.NONE;
        public MovementModifier Movement { get; set; }

        public virtual void OnEnterPower(PlayerCharacter character)
        {

        }

        public virtual void Step(PlayerCharacter character, float deltaTime)
        {

        }

        public virtual void OnExitPower(PlayerCharacter character)
        {

        }
    }
}