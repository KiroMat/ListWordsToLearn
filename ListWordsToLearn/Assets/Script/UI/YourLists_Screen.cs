using Assets.Script.Factories;
using Assets.Script.UI_Elements;
using ListWordsToLearn.Common.DB;
using ListWordsToLearn.Common.DB.Model;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Script.UI
{
    public class YourLists_Screen : UI_Screen
    {
        public ListElement templetaElementList;
        public GameObject Container;
        public Button AddNewListButton;

        IRepositoryDB<NameList> allListRepo;

        public override void StartScreen()
        {
            base.StartScreen();
            allListRepo = RepositoryFactory.GetRepozytory<NameList>();
            
            AddNewListButton.onClick.AddListener(() => { AddNewListToDb(); });
            RefreshList();
        }

        public void GoToListView(int idList)
        {
            // TODO: go to list view with ID as parameter
        }

        public override void CloseScreen()
        {
            base.CloseScreen();
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
                ele.NameList = elemlist.NameListOfWords;
                ele.ID = elemlist.ID;
                ele.onDelete = (e) => {
                        FindObjectOfType<MessageBox>().ShowYesNoWindow($"Czy napewno usuną liste o nazwie: {e.NameList}?", () => RemoveItemFromList(e));
                    };
                ele.onEdit = (e) => GoToListView(e.ID);
            }
            Vector2 vec = Container.GetComponent<RectTransform>().sizeDelta;
            vec.y = templetaElementList.GetComponent<RectTransform>().rect.height * allList.Count;
            Container.GetComponent<RectTransform>().sizeDelta = vec;
        }

        private void AddNewListToDb()
        {
            var messageBox = FindObjectOfType<MessageBox>();
            messageBox.ShowInputWindow("Proszę podać nazwę nowej listy", AfterConfirmNewList);
        }

        private void AfterConfirmNewList(string name)
        {
            if(!string.IsNullOrEmpty(name))
            {
                allListRepo.Insert(new NameList() { NameListOfWords = name });
                RefreshList();
            }
        }

        private void ClearList()
        {
            var listCilds = new List<Transform>();
            foreach (Transform child in Container.transform)
            {
                listCilds.Add(child);
            }

            Container.transform.DetachChildren();

            for (int i = listCilds.Count-1; i >= 0; i--)
            {
                Destroy(listCilds[i].gameObject);
            }
        }

        private void RemoveItemFromList(ListElement item)
        {
            allListRepo.DropCollection(item.NameList);
            var temp = allListRepo.GetById(item.ID);
            allListRepo.Remove(temp);
            RefreshList();
        }
    }
}
