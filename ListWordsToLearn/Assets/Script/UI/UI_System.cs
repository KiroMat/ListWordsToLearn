using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    public class UI_System : MonoBehaviour
    {
        [Header("Main Propeties")]
        public UI_Screen m_Start;

        [Header("System Events")]
        public UnityEvent onSwitchedScreen = new UnityEvent();

        [Header("Fader properties")]
        public Image m_Fader;
        public float m_FadeInDuration = 1f;
        public float m_fadeOutDuration = 1f;

        public Component[] screens = new Component[0];
        private UI_Screen currentScreen;
        public UI_Screen CurrentScreen
        {
            get { return currentScreen; }
        }

        private UI_Screen previousScreen;
        public UI_Screen PreviousScreen
        {
            get { return previousScreen; }
        }

        private List<UI_Screen> previousS = new List<UI_Screen>(5);



        private void Start()
        {
            screens = GetComponentsInChildren<UI_Screen>();
            InitializeScreens();


            if (m_Start)
            {
                SwitchScreen(m_Start);
            }

            if(m_Fader)
            {
                m_Fader.gameObject.SetActive(true);
            }

            FadeIn();
        }

        private void InitializeScreens()
        {
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(false);
            }
        }

        public void SwitchScreen(UI_Screen screen)
        {
            if(screen)
            {
                if (currentScreen)
                {
                    currentScreen.CloseScreen();
                    currentScreen.gameObject.SetActive(false);
                    AddPreviousScreen(currentScreen);
                }

                currentScreen = screen;
                currentScreen.gameObject.SetActive(true);
                currentScreen.StartScreen();

                if (onSwitchedScreen != null)
                {
                    onSwitchedScreen.Invoke();
                }
            }
        }

        public void FadeIn()
        {
            if (m_Fader)
            {
                m_Fader.CrossFadeAlpha(0f, m_FadeInDuration, false);
            }
        }

        public void FadeOut()
        {
            if (m_Fader)
            {
                m_Fader.CrossFadeAlpha(1f, m_fadeOutDuration, false);
            }
        }

        public void GoToPreviouseScreen()
        {
            SwitchScreen(GetPreviousScreen());
        }

        public void LoadScene(int sceneIndex)
        {
            StartCoroutine(WaitToLoadScene(sceneIndex)); 
        }

        public void ExitApp()
        {
            Application.Quit();
        }

        IEnumerator WaitToLoadScene(int sceneIndex)
        {
            yield return null;
        }

        private void AddPreviousScreen(UI_Screen screen)
        {
            if (previousS.Count < 5)
                previousS.Add(screen);
            else
            {
                previousS.RemoveAt(0);
                previousS.Add(screen);
            }
        }

        private UI_Screen GetPreviousScreen()
        {
            if (previousS.Count <= 0)
                return m_Start;

            var screen = previousS[previousS.Count - 1];
            previousS.RemoveAt(previousS.Count - 1);
            return screen;
        }
    }
}
