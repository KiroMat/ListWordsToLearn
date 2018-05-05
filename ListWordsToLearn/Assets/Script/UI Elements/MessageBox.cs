using Assets.Script.UI_Elements;
using System;
using UnityEngine;

namespace Assets.Script.UI_Elements
{
    public class MessageBox : MonoBehaviour
    {
        public ModalWindowInput InputWindow;
        public ModalWindowYesNo YesNoWindow;

        private void Start()
        {
            Initialized();
        }

        public void ShowInputWindow(string text, Action<string> action)
        {
            InputWindow.ShowWindow(text, action);
        }

        private void Initialized()
        {
            var allWindows = GetComponentsInChildren<ModalWindow>();
            foreach (var window in allWindows)
            {
                window.gameObject.SetActive(false);
            }

        }
    }
}

