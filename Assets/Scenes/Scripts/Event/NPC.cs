using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    //private TestChat testChat;
    DialogueBox dialogueBox;
    [SerializeField] private GameObject npc_Image;
    [SerializeField] GameObject potal;
    public bool isNpc;

    public void Active()
    {
        npc_Image.SetActive(true);
    }

    private void Awake()
    {
        //testChat = GetComponent<TestChat>();
        dialogueBox= GetComponent<DialogueBox>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && dialogueBox.isTrigger && potal != null)
            potal.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(isNpc)
            {
                dialogueBox.isTrigger = true;
            }
        }
    }
}
