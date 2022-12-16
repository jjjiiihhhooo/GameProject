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
    string a;

    [SerializeField] bool Bool;

    public void chap2_questText()
    {
        questcount += 1;
        qusetText.SetActive(true);
        string a = "놀이기구 타기 [" + questcount + "/4]";
        quest_Text.text = a;
    }    


    SceneChanger sceneChanger;

    private void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    public void ChangeItem(string _item, Sprite _image)
    {
        questItem = _item;
        questImage = _image;
        theQuestInventory.ItemChange(questItem, questImage);
        if (Bool)
        {
            table.pieceCount++;
            a = "찢어진 종이조각 [" + table.pieceCount + "/4]";
            quest_Text.text = a;

            if (table.pieceCount >= 4 && sceneChanger.currentScene == "Main")
            {
                a = "찢어진 종이조각 [" + table.pieceCount + "/4]";
                quest_Text.text = a;
                table.AcitveBook();
                field_book.SetActive(false);
                qusetText.SetActive(false);
            }
        }
    }


}
