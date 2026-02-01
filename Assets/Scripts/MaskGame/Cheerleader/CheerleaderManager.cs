using Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MaskGame.Cheerleader
{
    public class CheerleaderManager : MonoBehaviour
    {
        public float TimeBetweenBeatsSeconds = 1;

        protected Dictionary<MonoBehaviour, Action<CheerBeatCallbackInput>> BeatListeners = new Dictionary<MonoBehaviour, Action<CheerBeatCallbackInput>>();
        protected UpdateLoopTimer Timer = new UpdateLoopTimer();
        protected int LifetimeBeatCount = 0;
        protected int BeatCount = 0;
        protected float ElapsedTime = 0;

        public void RegisterBeatCallback(MonoBehaviour owner, Action<CheerBeatCallbackInput> callback)
        {
            BeatListeners[owner] = callback;
        }

        public void RemoveBeatCallback(MonoBehaviour owner)
        {
            BeatListeners.Remove(owner);
        }

        public void TriggerBeat(bool isMinor, bool isLeft, int count)
        {
            foreach (var pair in BeatListeners)
            {
                Action<CheerBeatCallbackInput> callback = pair.Value;
                CheerBeatCallbackInput input = new CheerBeatCallbackInput();
                input.IsMinorBeat = isMinor;
                input.IsMajorBeat = !isMinor;
                input.IsLeft = isLeft;
                input.StepCount = count;
                callback(input);
            }
        }

        public void Update()
        {
            ElapsedTime += Time.deltaTime;
            while (ElapsedTime > TimeBetweenBeatsSeconds)
            {
                ElapsedTime -= TimeBetweenBeatsSeconds;
                BeatCount += 1;
                LifetimeBeatCount += 1;

                switch (BeatCount)
                {
                    case 1:
                    case 2:
                        TriggerBeat(true, true, BeatCount);
                        break;
                    case 3:
                        TriggerBeat(false, true, BeatCount);
                        break;
                    case 4:
                    case 5:
                        TriggerBeat(true, false, BeatCount);
                        break;
                    case 6:
                        TriggerBeat(false, false, BeatCount);
                        break;
                    default:
                        break;
                }

                BeatCount %= 6;
            }
        }
    }
}