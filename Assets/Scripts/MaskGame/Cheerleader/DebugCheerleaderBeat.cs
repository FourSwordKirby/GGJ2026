using UnityEngine;

namespace MaskGame.Cheerleader
{
    public class DebugCheerleaderBeat : MonoBehaviour
    {
        public CheerleaderManager Manager;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Manager = FindObjectsByType<CheerleaderManager>(FindObjectsSortMode.InstanceID)[0];
            Manager.RegisterBeatCallback(this, HandleBeatCallback);
        }

        protected void HandleBeatCallback(CheerBeatCallbackInput input)
        {
            string beatType = input.IsMinorBeat ? "Minor" : "Major";
            string leftRight = input.IsLeft ? "Left" : "Right";
            Debug.Log($"Got Beat [{beatType}, {leftRight}], Step count: {input.StepCount}");
        }
    }
}