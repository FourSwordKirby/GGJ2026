using Core;
using System;
using UnityEngine;

namespace MaskGame.Character.Modifier
{
    [Serializable]
    public class ReversedWalkingMovementModifier : WalkingMovementModifier
    {
        protected override Vector3 ComputeWalkingMovementImpulse(MaskGameCharacter character, Vector3 intentedMove, float deltaTime)
        {
            return base.ComputeWalkingMovementImpulse(character, -intentedMove, deltaTime);
        }
    }
}