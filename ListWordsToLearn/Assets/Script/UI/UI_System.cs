using System;
using System.Collections;
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
                screen.gameObject.SetActive(true);
            }
        }

        public void SwitchScreen(UI_Screen screen)
        {
            if(screen)
            {
                if (currentScreen)
                {
                    currentScreen.CloseScreen();
                    previousScreen = currentScreen;
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
            if (previousScreen)
            {
                SwitchScreen(previousScreen);
            }
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
    }
}
