using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Script.UI_Elements
{
    public class ModalWindowYesNo : ModalWindow
    {
        private Action onClickConfirm;

        private void Start()
        {
            Confirm_Button.onClick.AddListener(() => ConfirmDecision());
            Cancel_Button.onClick.AddListener(() => Cancel());
        }

        public void ShowWindow(string text, Action action)
        {
            onClickConfirm = action;
            gameObject.SetActive(true);
            ShowTextObject.text = text;
        }

        private void Cancel()
        {
            gameObject.SetActive(false);
        }

        private void ConfirmDecision()
        {
            gameObject.SetActive(false);
            onClickConfirm.Invoke();
        }
    }
}

