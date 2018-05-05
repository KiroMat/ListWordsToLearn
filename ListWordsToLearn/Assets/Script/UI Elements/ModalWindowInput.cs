using Assets.Script.UI_Elements;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI_Elements
{
    public class ModalWindowInput : ModalWindow
    {
        public InputField InputTextObject;
        public string returnText;
        public string TextToShowing;
        public bool IsReady;
        private Action<string> onClickConfirm;
        
        // Use this for initialization
        void Start()
        {
            Confirm_Button.onClick.AddListener(() => { ClickComfirm(); });
            Cancel_Button.onClick.AddListener(() => { ClickClose(); });
        }

        public void ShowWindow(string text, Action<string> action)
        {
            IsReady = false;
            onClickConfirm = action;
            gameObject.SetActive(true);
            ShowTextObject.text = TextToShowing;
            InputTextObject.text = string.Empty;
            StartCoroutine(WaitToConfirm());
        }

        private void ClickComfirm()
        {
            IsReady = true;
        }

        private void ClickClose()
        {
            IsReady = true;
        }

        IEnumerator WaitToConfirm()
        {
            yield return new WaitUntil(() => IsReady == true);
            gameObject.SetActive(false);
            onClickConfirm.Invoke(InputTextObject.text);
        }
    }
}

