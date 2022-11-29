using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestManager : MonoBehaviour
{
    [SerializeField] private string questItem;
    [SerializeField] private Table table;
    [SerializeField] GameObject field_book;
    [SerializeField] private Sprite questImage;
    [SerializeField] private QuestInventory theQuestInventory;
    public Text quest_Text;
    public GameObject qusetText;
    public int questcount;

    public void chap2_questText()
    {
        questcount += 1;
        qusetText.SetActive(true);
        string a = "���̱ⱸ Ÿ�� [4/" + questcount + "]";
        quest_Text.text = a;
    }    


    public void ChangeItem(string _item, Sprite _image)
    {
        questItem = _item;
        questImage = _image;
        theQuestInventory.ItemChange(questItem, questImage);
        table.pieceCount++;
<<<<<<< HEAD


        if(table.pieceCount >=4 && sceneChanger.currentScene == "Main")

        string a = "������ ���� ���� [4/" + table.pieceCount + "]";
        quest_Text.text = a;

        if(table.pieceCount >=4)

=======
        if(table.pieceCount >=4)
>>>>>>> parent of 6c5fb86 (HSW 2022.11.28)
        {
            table.AcitveBook();
            field_book.SetActive(false);
            qusetText.SetActive(false);

        }
    }


}
