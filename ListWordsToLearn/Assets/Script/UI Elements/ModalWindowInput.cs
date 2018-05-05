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
        private Action<string> onClickConfirm;

        void Start()
        {
            Confirm_Button.onClick.AddListener(() => { ClickComfirm(); });
            Cancel_Button.onClick.AddListener(() => { ClickClose(); });
        }

        public void ShowWindow(string text, Action<string> action)
        {
            onClickConfirm = action;
            gameObject.SetActive(true);
            ShowTextObject.text = text;
            InputTextObject.text = string.Empty;
        }

        private void ClickComfirm()
        {
            onClickConfirm.Invoke(InputTextObject.text);
            gameObject.SetActive(false);
        }

        private void ClickClose()
        {
            gameObject.SetActive(false);
        }
    }
}

