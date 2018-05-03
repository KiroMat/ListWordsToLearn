using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.UI
{
    public class UI_WaitingScreen : UI_Screen
    {
        [Header("Time screen properties")]
        public float m_ScreenTime = 2f;
        public UnityEvent onTimeCompleted = new UnityEvent();

        private float startTime;

        public override void StartScreen()
        {
            base.StartScreen();
            startTime = Time.time;
            StartCoroutine(WaitForTime());
        }

        private IEnumerator WaitForTime()
        {
            yield return new WaitForSeconds(m_ScreenTime);

            if(onTimeCompleted != null)
            {
                onTimeCompleted.Invoke();
            }
        }
    }
}
