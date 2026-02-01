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
        public MaskPower CurrentMaskPower => MaskMap[CurrentMaskState];

        public Dictionary<MaskState, MaskPower> MaskMap { get; private set; } = new Dictionary<MaskState, MaskPower>()
        {
            { MaskState.BASIC, new BasicMaskPower() },
            { MaskState.CHEER, new CheerleaderMaskPower() },
            { MaskState.BUSINESS, new BusinessMaskPower() },
            { MaskState.NERD, new NerdMaskPower() },
            { MaskState.JOCK, new JockMaskPower() },
            { MaskState.THEATER, new TheaterMaskPower() },
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

            CurrentMaskPower.OnExitPower(Player);
            CurrentMaskState = NextMaskState;
            CurrentMaskPower.OnEnterPower(Player);
            NextMaskState = MaskState.NONE;

            CurrentMaskPower.Step(Player, deltaTime);

            switch (CurrentMaskState)
            {
                case MaskState.BUSINESS:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.blue;
                    }
                    break;
                case MaskState.CHEER:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.yellow;
                    }
                    break;
                case MaskState.NERD:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.green;
                    }
                    break;
                case MaskState.JOCK:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.red;
                    }
                    break;
                case MaskState.THEATER:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.black;
                    }
                    break;
                case MaskState.BASIC:
                default:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
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