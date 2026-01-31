using System.Collections;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// A template Behavior that splits FixedUpdate into different tick groups (phases of execution).
    /// See <seealso cref="ActorTickGroup"/>.
    /// </summary>
    public class FixedUpdateActor : MonoBehaviour
    {
        [Header(nameof(FixedUpdateActor) + " Config")]
        public bool VerboseLogTickGroup = false;
        public bool EnableLateFixedUpdate = true;
        public ActorTickGroup CurrentTickGroup = ActorTickGroup.Unknown;
        public uint GlobalFixedUpdateCount = 0;
        protected Coroutine LateFixedUpdateCoroutine;

        #region Unity Callback Hooks
        protected virtual void Awake()
        {
            // No implementation, but objects cast to FixedUpdateActor base may need access to this function.
        }

        protected virtual void Start()
        {
            // No implementation, but objects cast to FixedUpdateActor base may need access to this function.
        }

        protected virtual void OnValidate()
        {
            // No implementation, but objects cast to FixedUpdateActor base may need access to this function.
        }

        protected virtual void OnEnable()
        {
            if (LateFixedUpdateCoroutine != null)
            {
                // Prevent duplicates in case the coroutine was somehow lingering.
                StopCoroutine(LateFixedUpdateCoroutine);
                LateFixedUpdateCoroutine = null;
            }

            if (EnableLateFixedUpdate)
            {
                LateFixedUpdateCoroutine = StartCoroutine(LateFixedUpdate());
            }
        }

        protected virtual void OnDisable()
        {
            // By default, coroutines are only stopped if the whole GameObject is deactivated.
            // Put this here to stop the Coroutine when this MonoBehaviour is disabled.
            if (LateFixedUpdateCoroutine != null)
            {
                StopCoroutine(LateFixedUpdateCoroutine);
                LateFixedUpdateCoroutine = null;
            }
        }

        protected virtual void FixedUpdate()
        {
            ++GlobalFixedUpdateCount;
            float deltaTime = Time.deltaTime;

            if (!EnableLateFixedUpdate)
            {
                CurrentTickGroup = ActorTickGroup.PostPhysics;
                LogTickGroup();
                PostPhysics(deltaTime);
            }

            CurrentTickGroup = ActorTickGroup.PrePhysics;
            LogTickGroup();
            PrePhysics(deltaTime);
            CurrentTickGroup = ActorTickGroup.DuringAnimation;
        }

        // Pooling these to avoid extra allocs per the advice of the following thread
        // https://forum.unity.com/threads/when-we-need-new-instance-of-intrinsic-yieldinstruction.1014100/
        private WaitForFixedUpdate FixedUpdateYieldInstruction = new WaitForFixedUpdate();

        /// <summary>
        /// Coroutine to run code after FixedUpdate but before the game frame renders.
        /// </summary>
        protected virtual IEnumerator LateFixedUpdate()
        {
            while (true)
            {
                yield return FixedUpdateYieldInstruction;

                float deltaTime = Time.deltaTime;
                CurrentTickGroup = ActorTickGroup.PostPhysics;
                LogTickGroup();
                PostPhysics(deltaTime);
                CurrentTickGroup = ActorTickGroup.Render;
            }
        }
        #endregion

        /// <summary>
        /// Runs before the animator and physics update.
        /// </summary>
        public virtual void PrePhysics(float deltaTime)
        {
        }

        /// <summary>
        /// Runs after the physics step but before render.
        /// All physics events should have triggered by this point (e.g. OnTriggerXXX and OnCollisionXXX).
        /// </summary>
        public virtual void PostPhysics(float deltaTime)
        {
        }

        /// <summary>
        /// Writes the current tick group to the log.
        /// </summary>
        public virtual void LogTickGroup()
        {
            LogVerbose($"Tick Group = {CurrentTickGroup}");
        }

        #region Custom Logging
        public void Log(string s)
        {
            Debug.Log(FormatLogMessage(s));
        }

        public void LogVerbose(string s)
        {
            if (VerboseLogTickGroup)
            {
                Log(s);
            }
        }

        public void LogError(string s)
        {
            Debug.LogError(FormatLogMessage(s));
        }

        public void LogWarning(string s)
        {
            Debug.LogWarning(FormatLogMessage(s));
        }

        protected string FormatLogMessage(string s)
        {
            return $"[{gameObject.name}] [{GlobalFixedUpdateCount}|{CurrentTickGroup}] : {s}";
        }

        public static void LogError(MonoBehaviour b, string s)
        {
            FixedUpdateActor actor = b.GetComponent<FixedUpdateActor>();
            if (actor != null)
            {
                actor.LogError(s);
            }
            else
            {
                Debug.LogError($"[{b.gameObject.name}] : {s}");
            }
        }
        #endregion
    }
}