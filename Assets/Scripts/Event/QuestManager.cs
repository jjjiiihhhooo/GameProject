using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [SerializeField] private string questItem;
    [SerializeField] private Table table;
    [SerializeField] private Sprite qusetImage;
    [SerializeField] private QuestInventory theQuestInventory;

    public void ChangeItem(string _item, Sprite _image)
    {
        questItem = _item;
        qusetImage = _image;

        theQuestInventory.ItemChange(questItem, qusetImage);
        table.pieceCount++;
        if(table.pieceCount >=4)
        {
            table.AcitveBook();
        }
    }


}
