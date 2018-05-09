using Assets.Script.Factories;
using Assets.Script.UI;
using ListWordsToLearn.Common.DB;
using ListWordsToLearn.Common.DB.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Assets.Script.UI_Elements
{
    public class WordsInList_Screen : UI_Screen
    {
        public GameObject Container;
        public ListElement templetaElementList;
        public Button AddButton;
        public string NameOfList;
        public DetailsWord_Screen DetailsScreen;
        public Text MainText;

        IRepositoryDB<WordDetailModel> allListRepo;

        public override void StartScreen()
        {
            base.StartScreen();
            MainText.text = NameOfList;
            AddButton.onClick.AddListener(() => AddNewword());
            allListRepo = RepositoryFactory.GetRepozytory<WordDetailModel>(NameOfList.Replace(' ', '_'));
            RefreshList();
        }

        public override void CloseScreen()
        {
            base.CloseScreen();
        }

        public void GoToCheckWords(CheckWord_Screen screen)
        {
            screen.NameList = NameOfList;
            FindObjectOfType<UI_System>().SwitchScreen(screen);
        }

        private void RefreshList()
        {
            if (Container.transform.childCount > 0)
                ClearList();

            var allList = allListRepo.GetAll().ToList();
            foreach (var elemlist in allList)
            {
                var ele = Instantiate(templetaElementList);
                ele.transform.SetParent(Container.transform);
                ele.NameList = elemlist.NameWordPl;
                ele.ID = elemlist.ID;
                ele.onDelete = (e) => {
                    FindObjectOfType<MessageBox>().ShowYesNoWindow($"Czy napewno usuną słówko: {e.NameList}?", () => RemoveItemFromList(e));
                };
                ele.onEdit = (e) => GoToDetailView(e.ID);
            }
            Vector2 vec = Container.GetComponent<RectTransform>().sizeDelta;
            vec.y = templetaElementList.GetComponent<RectTransform>().rect.height * allList.Count;
            Container.GetComponent<RectTransform>().sizeDelta = vec;
        }

        private void GoToDetailView(int id)
        {
            DetailsScreen.IdItem = id;
            DetailsScreen.NameList = NameOfList;
            DetailsScreen.isEdit = true;
            DetailsScreen.OwnerListScreen = this;
            FindObjectOfType<UI_System>().SwitchScreen(DetailsScreen);
        }

        private void ClearList()
        {
            var listCilds = new List<Transform>();
            foreach (Transform child in Container.transform)
            {
                listCilds.Add(child);
            }

            Container.transform.DetachChildren();

            for (int i = listCilds.Count - 1; i >= 0; i--)
            {
                Destroy(listCilds[i].gameObject);
            }
        }

        private void RemoveItemFromList(ListElement item)
        {
            var temp = allListRepo.GetById(item.ID);
            allListRepo.Remove(temp);
            RefreshList();
        }

        private void AddNewword()
        {
            DetailsScreen.isEdit = false;
            DetailsScreen.NameList = NameOfList;
            DetailsScreen.OwnerListScreen = this;
            FindObjectOfType<UI_System>().SwitchScreen(DetailsScreen);
        }

    }
}

