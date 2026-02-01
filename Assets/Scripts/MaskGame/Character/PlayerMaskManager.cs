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

        protected NPCSettings npcSettings => GameManager.instance?.NPCSettings;

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
            RefreshTint();
        }

        public void QueueNextMaskState(MaskState nextMaskState)
        {
            NextMaskState = nextMaskState;
        }

        public void Step(float deltaTime)
        {
            if (NextMaskState == MaskState.NONE)
            {
                CurrentMaskPower.Step(Player, deltaTime);
                return;
            }

            CurrentMaskPower.OnExitPower(Player);
            CurrentMaskState = NextMaskState;
            CurrentMaskPower.OnEnterPower(Player);
            NextMaskState = MaskState.NONE;

            CurrentMaskPower.Step(Player, deltaTime);

            RefreshTint();
        }

        void RefreshTint()
        {
            if (!npcSettings)
                return;

            Color targetColor = Color.white;

            if (CurrentMaskState != MaskState.BASIC)
            {
                targetColor = npcSettings.ColorFromMask(CurrentMaskState);
                targetColor = Color.Lerp(targetColor, Color.white, 0.5f);
            }

            foreach (SkinnedMeshRenderer m in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                m.materials[0].color = targetColor;
            }
        }

        public MovementModifier GetCurrentMovementMondifier()
        {
            return MaskMap[CurrentMaskState].Movement;
        }
    }
}