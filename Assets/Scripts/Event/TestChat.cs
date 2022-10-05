using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChat : MonoBehaviour
{
    private ChatManager theChatManager;
    public Chat chat;

    public bool isTrigger = false;
    public bool isTarget = true;

    void Start()
    {
        theChatManager = FindObjectOfType<ChatManager>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTrigger = true;   
        }
    }

    private void Update()
    {
        if(isTarget && isTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            Chat();
            isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTrigger = false;
        }
    }


    private void Chat()
    {
        theChatManager.ShowChat(chat);
    }
}
