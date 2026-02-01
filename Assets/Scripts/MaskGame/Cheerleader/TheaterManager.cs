using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaskGame.Theater
{
    public class TheaterManager : MonoBehaviour
    {
        public struct CallbackInput
        {
            public bool IsLeft;
        }

        public static float TimeBetweenBeatsSeconds = 1;

        protected Dictionary<MonoBehaviour, Action<CallbackInput>> SwitchListeners = new Dictionary<MonoBehaviour, Action<CallbackInput>>();
        protected UpdateLoopTimer Timer = new UpdateLoopTimer();
        protected int SwitchCount = 0;
        protected float ElapsedTime = 0;

        public void RegisterCallback(MonoBehaviour owner, Action<CallbackInput> callback)
        {
            SwitchListeners[owner] = callback;
        }

        public void RemoveCallback(MonoBehaviour owner)
        {
            SwitchListeners.Remove(owner);
        }

        public void TriggerSwitch(bool isLeft)
        {
            foreach (var pair in SwitchListeners)
            {
                Action<CallbackInput> callback = pair.Value;
                CallbackInput input = new CallbackInput();
                input.IsLeft = isLeft;
                callback(input);
            }
        }

        public void PhaseCur(out bool isLeft, out float uTilSwitch)
        {
            // For syncing the theater kids if they enabled mid-phase

            uTilSwitch = ElapsedTime / TimeBetweenBeatsSeconds;

            switch (SwitchCount)
            {
                case 0: isLeft = true; break;
                case 1: isLeft = false; break;
                default: isLeft = true; break;
            }
        }

        public void Update()
        {
            ElapsedTime += Time.deltaTime;
            while (ElapsedTime > TimeBetweenBeatsSeconds)
            {
                ElapsedTime -= TimeBetweenBeatsSeconds;
                SwitchCount += 1;
                SwitchCount %= 2;

                switch (SwitchCount)
                {
                    case 0:
                        TriggerSwitch(true);
                        break;
                    case 1:
                        TriggerSwitch(false);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}