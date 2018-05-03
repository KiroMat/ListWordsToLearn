using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UI_Screen : MonoBehaviour
    {
        [Header("Main properties")]
        public Selectable m_StartSelectable;

        [Header("Screen events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();

            if(m_StartSelectable)
            {
                EventSystem.current.SetSelectedGameObject(m_StartSelectable.gameObject);
            }
        }

        public virtual void StartScreen()
        {
            if (onScreenStart != null)
            {
                onScreenStart.Invoke();
            }

            HandleAnimator("show");
        }

        public virtual void CloseScreen()
        {
            if (onScreenClose != null)
            {
                onScreenClose.Invoke();
            }

            HandleAnimator("hide");
        }

        void HandleAnimator(string nameTrigger)
        {
            if (animator)
            {
                animator.SetTrigger(nameTrigger);
            }
        }
    }
}
