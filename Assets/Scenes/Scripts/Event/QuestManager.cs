using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [SerializeField] private string questItem;
    [SerializeField] private Table table;
    [SerializeField] GameObject field_book;
    [SerializeField] private Sprite qusetImage;
    [SerializeField] private QuestInventory theQuestInventory;

    SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    public void ChangeItem(string _item, Sprite _image)
    {
        questItem = _item;
        qusetImage = _image;

        theQuestInventory.ItemChange(questItem, qusetImage);
        table.pieceCount++;

        if(table.pieceCount >=4 && sceneChanger.currentScene == "Main")
        {
            table.AcitveBook();
            field_book.SetActive(false);
        }
    }


}
