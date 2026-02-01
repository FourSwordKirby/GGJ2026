using Core;
using Unity.VisualScripting;
using UnityEngine;

namespace MaskGame
{
    public class TempUIManager : MonoBehaviour
    {
        public GameObject Win;
        public GameObject LossPopularity;
        public GameObject LossTime;
        public GameObject StartTitle;

        protected UpdateLoopTimer Timer = new UpdateLoopTimer();

        private void OnValidate()
        {
        }

        private void Start()
        {
            Transform canvas = transform.Find("Canvas");

            Win = canvas.Find("Win").gameObject;
            LossPopularity = canvas.Find("LossPopularity").gameObject;
            LossTime = canvas.Find("LossTime").gameObject;
            StartTitle = canvas.Find("Start").gameObject;
        }

        private void Update()
        {
            if (Timer.Step(Time.deltaTime))
            {

                Timer.Disable();
            }
        }

        public void DisplayWin()
        {
            Timer.SetTimer(5);
        }

    }
}