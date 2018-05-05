using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Script.UI_Elements
{
    public class ListElement : MonoBehaviour
    {
        public Text DisplayedText;
        public Button EditListButtom;
        public Button DeleteListButton;
        public string NameList;
        public int ID;

        public Action<ListElement> onDelete;
        public Action<ListElement> onEdit;

        // Use this for initialization
        void Start()
        {
            DeleteListButton.onClick.AddListener(() => onDelete.Invoke(this));
            EditListButtom.onClick.AddListener(() => onEdit.Invoke(this));
            DisplayedText.text = NameList;
        }
    }
}



