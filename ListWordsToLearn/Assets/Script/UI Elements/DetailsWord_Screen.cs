using Assets.Script.Factories;
using Assets.Script.UI;
using ListWordsToLearn.Common.DB;
using ListWordsToLearn.Common.DB.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    public class DetailsWord_Screen : UI_Screen
    {
        public Text MainText;
        public Button ConfirmBtn;
        public Button BackBtn;
        public InputField PlWord;
        public InputField EnWord;
        public InputField AddInfo;
        public UI_Screen OwnerListScreen;

        public string NameList;
        public int IdItem;
        public bool isEdit;

        IRepositoryDB<WordDetailModel> wordRepo;

        private void Start()
        {
            ConfirmBtn.onClick.AddListener(() => Confirm());

        }

        public override void StartScreen()
        {
            base.StartScreen();
            BackBtn.onClick.AddListener(() => FindObjectOfType<UI_System>().SwitchScreen(OwnerListScreen));
            wordRepo = RepositoryFactory.GetRepozytory<WordDetailModel>(NameList.Replace(' ', '_'));

            if (isEdit)
            {
                var model = wordRepo.GetById(IdItem);
                AsEdit(model);
            }
            else
                AsAdd();
        }

        public override void CloseScreen()
        {
            base.CloseScreen();
        }

        private void AsEdit(WordDetailModel model)
        {
            MainText.text = "Edycja";
            PlWord.text = model.NameWordPl;
            EnWord.text = model.NameWordEn;
            AddInfo.text = model.AdditionalInfo;

            ConfirmBtn.GetComponentInChildren<Text>().text = "Zapisz zmiany";
        }

        private void AsAdd()
        {
            IdItem = 0;
            PlWord.text = string.Empty;
            EnWord.text = string.Empty;
            AddInfo.text = string.Empty;
            MainText.text = "Dodanie nowego słowa";
            ConfirmBtn.GetComponentInChildren<Text>().text = "Dodaj słówko";
        }

        private void Confirm()
        {
            var newModel = new WordDetailModel()
            {
                NameWordPl = PlWord.text,
                NameWordEn = EnWord.text,
                AdditionalInfo = AddInfo.text,
                ID = IdItem
            };

            if (isEdit)
                wordRepo.Update(newModel);
            else
                wordRepo.Insert(newModel);

            FindObjectOfType<UI_System>().SwitchScreen(OwnerListScreen);

        }
    }
}


