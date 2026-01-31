using MaskGame.Character.Modifier;
using System;
using UnityEngine;

namespace MaskGame.Character
{
    [Serializable]
    public class MaskPower
    {
        public virtual MaskState MaskState => MaskState.NONE;
        public MovementModifier Movement { get; set; }
    }
}