using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEvent : MonoBehaviour
{
    public Choice choice;
    public bool isTrigger = true;
    [SerializeField] private ChoiceManager theChoiceManager;
    [SerializeField] private QuestManager theQuestManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform mapTransform;
    [SerializeField] private Sprite quest_image;
    [SerializeField] private string quest_item;
    [SerializeField] private int check = 0; //0이면 맵 이동 하는 NPC , 1이면 상호작용 아이템(액자, 침대, 등등)


    private void OnTriggerEnter2D(Collider2D collision)
    {        
        theChoiceManager.isChoice = true;
        theChoiceManager.NpcChange(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        theChoiceManager.isChoice = false;
        StopCoroutine(ACoroutine());
    }

    public void StartCoroutine()
    {
        StartCoroutine(ACoroutine());
    }

    public IEnumerator ACoroutine()
    {
        theChoiceManager.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoiceManager.choiceIng);
    }

    public void Action(int _result)
    {
        if(_result == 0 && check == 0)
        {
            player.transform.position = mapTransform.position;
        }
        else if(_result == 0 && check == 1)
        {
            if(quest_image != null)
            {
                theQuestManager.ChangeItem(quest_item, quest_image);
            }
            this.gameObject.SetActive(false);
        }
    }
}
