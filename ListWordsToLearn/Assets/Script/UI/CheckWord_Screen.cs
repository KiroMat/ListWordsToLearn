using Assets.Script.Factories;
using Assets.Script.UI;
using ListWordsToLearn.Common.DB;
using ListWordsToLearn.Common.DB.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using ListWordsToLearn.Common.Extensions;
using System;

namespace Assets.Script.UI
{
    public class CheckWord_Screen : UI_Screen
    {
        public Button ChoiseButton;
        public Button StartButton;
        public Button CheckButton;
        public Button ConfirmButton;
        public GameObject CheckButtonBar;
        public GameObject StartPanel;
        public GameObject MainPanel;
        public GameObject Finalpanel;
        public Text ShowingText;
        public Text TransText;
        public Text DetailsText;
        public Text CouterText;
        public Text CorrectAnswerts;
        public Text WrongAnswerts;


        public string NameList;

        private IRepositoryDB<WordDetailModel> allListRepo;
        private List<WordDetailModel> wordsToCheck;
        private bool transToWord;
        private int currentWord;
        private List<bool> answersList;

        public override void StartScreen()
        {
            base.StartScreen();
            currentWord = 0;
            ChangeTextOnChangeButton();
            ClearScreen();
            StartButton.gameObject.SetActive(true);
            StartPanel.gameObject.SetActive(true);
            ChoiseButton.gameObject.SetActive(true);
            allListRepo = RepositoryFactory.GetRepozytory<WordDetailModel>(NameList.Replace(' ', '_'));
            wordsToCheck = allListRepo.GetAll().ToList();
            wordsToCheck.Shuffle();
            answersList = new List<bool>();
            ChangeCounter();
        }

        public override void CloseScreen()
        {
            base.CloseScreen();
        }

        public void ChangeWayToCheck()
        {
            transToWord = !transToWord;
            ChangeTextOnChangeButton();
        }

        public void StartChecking()
        {
            ClearScreen();
            MainPanel.gameObject.SetActive(true);
            ShowWord();
        }

        public void NextWord()
        {

            if (currentWord + 1 < wordsToCheck.Count)
            {
                currentWord++;
                ShowWord();
                ChangeCounter();
            }
            else
                ShowFinaleScreen();

        }


        //public void PreviousWord()
        //{
        //    if(currentWord - 1 >= 0)
        //    {
        //        currentWord--;
        //        ShowWord();
        //        ChangeCounter();
        //    }
        //}

        public void CheckWord()
        {
            TransText.gameObject.SetActive(true);
            DetailsText.gameObject.SetActive(true);
            CheckButtonBar.gameObject.SetActive(true);
            CheckButton.gameObject.SetActive(false);
        }

        public void ClickYes()
        {
            answersList.Add(true);
            NextWord();
        }

        public void ClickNo()
        {
            answersList.Add(false);
            NextWord();
        }

        private void ShowFinaleScreen()
        {
            ClearScreen();
            CorrectAnswerts.text = answersList.Count(a => a == true).ToString();
            WrongAnswerts.text = answersList.Count(a => a == false).ToString();
            Finalpanel.gameObject.SetActive(true);
            ConfirmButton.gameObject.SetActive(true);
        }

        public void ConfirmResult()
        {
            FindObjectOfType<UI_System>().GoToPreviouseScreen();
        }

        private void ChangeTextOnChangeButton()
        {
            if (transToWord)
                ChoiseButton.GetComponentInChildren<Text>().text = "Tłumaczenie -> Słówko";
            else
                ChoiseButton.GetComponentInChildren<Text>().text = "Słówko -> Tłumaczenie";
        }

        private void ShowWord()
        {
            HideTransAndDetails();

            if (transToWord)
                ShowFirsTranslateWord();
            else
                ShowFirstNormalWord();
        }

        private void ShowFirstNormalWord()
        {
            ShowingText.text = wordsToCheck[currentWord].NameWordPl;
            TransText.text = wordsToCheck[currentWord].NameWordEn;
            DetailsText.text = wordsToCheck[currentWord].AdditionalInfo;
        }

        private void ShowFirsTranslateWord()
        {
            ShowingText.text = wordsToCheck[currentWord].NameWordEn;
            TransText.text = wordsToCheck[currentWord].NameWordPl;
            DetailsText.text = wordsToCheck[currentWord].AdditionalInfo;
        }

        private void HideTransAndDetails()
        {
            TransText.gameObject.SetActive(false);
            DetailsText.gameObject.SetActive(false);
            CheckButtonBar.gameObject.SetActive(false);
            CheckButton.gameObject.SetActive(true);
        }

        private void ChangeCounter()
        {
            CouterText.text = $"{currentWord + 1} / {wordsToCheck.Count}";
        }

        private void ClearScreen()
        {
            StartButton.gameObject.SetActive(false);
            ChoiseButton.gameObject.SetActive(false);
            ConfirmButton.gameObject.SetActive(false);
            CheckButton.gameObject.SetActive(false);
            CheckButtonBar.gameObject.SetActive(false);
            MainPanel.gameObject.SetActive(false);
            StartPanel.gameObject.SetActive(false);
            Finalpanel.gameObject.SetActive(false);
        }
    }
}


