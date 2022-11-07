using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject idle_bed;
    [SerializeField] private GameObject start_fade;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject npc2;
    [SerializeField] private GameObject test;
    [SerializeField] private TestChat testChat;
    [SerializeField] private Sprite quest_image;
    [SerializeField] private string quest_item;
    [SerializeField] private int check = 0; //0이면 맵 이동 하는 NPC , 1이면 상호작용 아이템(액자, 침대, 등등)

    public bool goEvent = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        theChoiceManager.isChoice = true;
        theChoiceManager.NpcChange(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (test != null)
            isTrigger = true;
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

    // 선택 이후
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
        else if (check == 2)
        {
            goEvent = true;
        }
        else if(check == 3)
        {
            player.transform.position = new Vector2(-5.7f, -3.5f);
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            player.GetComponent<PlayerMove>().isMove = false;
            bed.GetComponent<Bed>().isBed = false;
            
            idle_bed.SetActive(true);
            idle_bed.GetComponent<BoxCollider2D>().isTrigger = true;
            idle_bed.GetComponent<Bed>().isBed = true;
            start_fade.SetActive(true);
            npc.GetComponent<TestChat>().isTarget = true;
            npc.GetComponent<NPC>().Active();
            bed.SetActive(false);
            npc2.SetActive(false);
        }
        else if(check == 4)
        {
            if (quest_image != null)
            {
                theQuestManager.ChangeItem(quest_item, quest_image);
                //testChat.Chat();
            }
            this.gameObject.SetActive(false);
        }
    }

    public void SetIsChoice2(bool _bool)
    {
        theChoiceManager.isChoice2 = _bool;
    }

    public bool GetIsChoice2()
    {
        return theChoiceManager.isChoice2;
    }
}
