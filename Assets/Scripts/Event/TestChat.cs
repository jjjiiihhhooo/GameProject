using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChat : MonoBehaviour
{
    private ChatManager theChatManager;
    public Chat chat;

    void Start()
    {
        theChatManager = FindObjectOfType<ChatManager>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            theChatManager.isChat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            theChatManager.isChat = false;
        }
    }

    public void OpenChat()
    {
        theChatManager.ShowChat(chat);
    }
}
