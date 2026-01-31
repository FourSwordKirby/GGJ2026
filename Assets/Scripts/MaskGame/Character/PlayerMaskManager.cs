using MaskGame.Character.Mask;
using MaskGame.Character.Modifier;
using System.Collections.Generic;
using UnityEngine;

namespace MaskGame.Character
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerMaskManager : MonoBehaviour
    {
        public PlayerCharacter Player { get; private set; }

        public MaskState NextMaskState;
        public MaskState CurrentMaskState = MaskState.BASIC;

        public Dictionary<MaskState, MaskPower> MaskMap { get; private set; } = new Dictionary<MaskState, MaskPower>()
        {
            { MaskState.BASIC, new BasicMaskPower() },
            { MaskState.BUSINESS, new BusinessMaskPower() },
        };

        private void OnValidate()
        {
            Player = GetComponent<PlayerCharacter>();
        }

        public void QueueNextMaskState(MaskState nextMaskState)
        {
            NextMaskState = nextMaskState;
        }

        public void Step(float deltaTime)
        {
            if (NextMaskState == MaskState.NONE)
            {
                return;
            }

            CurrentMaskState = NextMaskState;
            NextMaskState = MaskState.NONE;

            switch (CurrentMaskState)
            {
                case MaskState.BUSINESS:
                    foreach (MeshRenderer m in Player.GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.blue;
                    }
                    break;
                case MaskState.BASIC:
                default:
                    foreach (MeshRenderer m in Player.GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.white;
                    }
                    break;
            }
        }

        public MovementModifier GetCurrentMovementMondifier()
        {
            return MaskMap[CurrentMaskState].Movement;
        }
    }
}