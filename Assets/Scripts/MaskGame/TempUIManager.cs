using Core;
using System;
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
        public GameObject Alert;

        protected UpdateLoopTimer Timer = new UpdateLoopTimer();
        protected Action Callback;
        protected bool ShowAlert = false;

        private void OnValidate()
        {
        }

        private void Awake()
        {
            Transform canvas = transform.Find("Canvas");

            Win = canvas.Find("Win").gameObject;
            LossPopularity = canvas.Find("LossPopularity").gameObject;
            LossTime = canvas.Find("LossTime").gameObject;
            StartTitle = canvas.Find("Start").gameObject;
            Alert = canvas.Find("MaskMismatch").gameObject;
            HideAll();
        }

        private void Update()
        {
            if (Timer.Step(Time.deltaTime))
            {
                HideAll();
                if (Callback != null)
                {
                    Callback();
                    Callback = null;
                }
                Timer.Disable();
            }

            if (ShowAlert != Alert.activeSelf)
            {
                Alert.SetActive(ShowAlert);
                ShowAlert = false;
            }
        }

        public static void DisplayWin(Action callback)
        {
            TempUIManager manager = GameObject.FindAnyObjectByType<TempUIManager>();
            if (manager.Timer.IsEnabled)
            {
                return;
            }
            manager.Timer.SetTimer(5);
            manager.Win.SetActive(true);
            manager.Callback = callback;
        }
        public static void DisplayLossPopularity(Action callback)
        {
            TempUIManager manager = GameObject.FindAnyObjectByType<TempUIManager>();
            if (manager.Timer.IsEnabled)
            {
                return;
            }
            manager.Timer.SetTimer(5);
            manager.LossPopularity.SetActive(true);
            manager.Callback = callback;
        }
        public static void DisplayLossTime(Action callback)
        {

            TempUIManager manager = GameObject.FindAnyObjectByType<TempUIManager>();
            if (manager.Timer.IsEnabled)
            {
                return;
            }
            manager.Timer.SetTimer(5);
            manager.LossTime.SetActive(true);
            manager.Callback = callback;
        }
        public static void DisplayStartTitle(Action callback)
        {
            TempUIManager manager = GameObject.FindAnyObjectByType<TempUIManager>();
            if (manager.Timer.IsEnabled)
            {
                return;
            }
            manager.Timer.SetTimer(5);
            manager.StartTitle.SetActive(true);
            manager.Callback = callback;
        }

        public void HideAll()
        {
            Win.SetActive(false);
            LossPopularity.SetActive(false);
            LossTime.SetActive(false);
            StartTitle.SetActive(false);
            Alert.SetActive(false);
        }

        public static void ShowMismatchAlert()
        {
            TempUIManager manager = GameObject.FindAnyObjectByType<TempUIManager>();
            manager.ShowAlert = true;
        }
    }
}