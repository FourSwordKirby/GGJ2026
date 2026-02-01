using UnityEngine;

namespace MaskGame.Cheerleader
{
    public class DebugCheerleaderBeat : MonoBehaviour
    {
        public CheerleaderManager Manager;

        public Transform[] BeatLocations = new Transform[7];
        public Transform Arrow;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Manager = FindObjectsByType<CheerleaderManager>(FindObjectsSortMode.InstanceID)[0];
            Manager.RegisterBeatCallback(this, HandleBeatCallback);

            Transform canvas = transform.Find("Canvas");
            if (canvas != null)
            {
                Transform panel = canvas.Find("Panel");
                BeatLocations[1] = panel.Find("Beat1");
                BeatLocations[2] = panel.Find("Beat2");
                BeatLocations[3] = panel.Find("Beat3");
                BeatLocations[4] = panel.Find("Beat4");
                BeatLocations[5] = panel.Find("Beat5");
                BeatLocations[6] = panel.Find("Beat6");
                Arrow = panel.Find("Arrow");
            }
        }

        protected void HandleBeatCallback(CheerBeatCallbackInput input)
        {
            string beatType = input.IsMinorBeat ? "Minor" : "Major";
            string leftRight = input.IsLeft ? "Left" : "Right";
            Debug.Log($"Got Beat [{beatType}, {leftRight}], Step count: {input.StepCount}");
            Vector3 newPos = Arrow.transform.position;
            newPos.x = BeatLocations[input.StepCount].position.x;
            Arrow.transform.position = newPos;
        }
    }
}