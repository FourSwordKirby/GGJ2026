using System;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// A simple management class to help measure elapsed time during Update() or FixedUpdate() functions.
    /// This wraps up the code to do the "elapsedTime += Time.deltaTime" logic.
    /// </summary>
    [Serializable]
    public class UpdateLoopTimer
    {
        public bool IsEnabled { get; protected set; } = false;

        public float ElapsedTime { get; protected set; } = 0.0f;
        public float TargetTime { get; protected set; } = 0.0f;

        /// <summary>
        /// Expresses the time as a normalized value between 0 and 1, indicating the progress to TargetTime.
        /// </summary>
        public float NormalizedTime
        {
            get
            {
                if (TargetTime == 0.0f)
                {
                    return 1.0f;
                }
                else
                {
                    return ElapsedTime / TargetTime;
                }
            }
        }

        public int Steps { get; protected set; } = 0;

        /// <summary>
        /// Set to true if the first call to Step() should not advance the elapsed time.
        /// You should enable this functionality if you will set the timer and call step in the same frame.
        /// </summary>
        protected bool SkipFirstStep = true;

        /// <summary>
        /// When true, Elapsed time will max out at TargetTime. (i.e. Normalized time will never be > 1)
        /// </summary>
        protected bool SaturateElapsedTime = true;

        /// <param name="skipFirstStep">Set to true if the first call to Step() should not advance the elapsed time.
        /// You should enable this functionality if you will set the timer and call step in the same frame.
        /// Defaults to true, because generally setting the timer is considered part of the upcoming game frame.</param>
        /// <param name="saturateElapsedTime">When true, the elapsed time will max out at TargetTime. (i.e. Normalized time will never be > 1)</param>
        public UpdateLoopTimer(bool skipFirstStep = true, bool saturateElapsedTime = true)
        {
            SkipFirstStep = skipFirstStep;
            SaturateElapsedTime = saturateElapsedTime;
        }

        /// <summary>
        /// Disables the timer, preventing Step() from advancing,
        /// </summary>
        public void Disable()
        {
            IsEnabled = false;
        }

        /// <summary>
        /// Resets the elapsed time to 0 and sets the target time to wait.
        /// Also enables the timer.
        /// </summary>
        /// <param name="time"></param>
        public void SetTimer(float time, bool skipFirstStep = true)
        {
            ElapsedTime = 0.0f;
            TargetTime = time;
            Steps = 0;
            IsEnabled = true;
            SkipFirstStep = skipFirstStep;
        }

        /// <summary>
        /// Advance the timer by the specified time if enabled. Usually Time.deltaTime.
        /// Returns true if the target time is reached after this step.
        /// Always returns false if the Timer is disabled.
        /// </summary>
        public bool Step(float deltaTime)
        {
            if (!IsEnabled)
            {
                return false;
            }

            if (!SkipFirstStep || Steps != 0)
            {
                ElapsedTime += deltaTime;
                if (ElapsedTime > TargetTime)
                {
                    ElapsedTime = TargetTime;
                }
            }

            ++Steps;

            return IsDone;
        }

        /// <summary>
        /// True if enabled and the target amount of time has elapsed.
        /// </summary>
        public bool IsDone => IsEnabled && ElapsedTime >= TargetTime; // Use equality because if target time is 0, then we are done. Also use greater-than for if the "saturate" option is not enabled.
    }
}