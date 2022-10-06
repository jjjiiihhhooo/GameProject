using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestChat : MonoBehaviour
{
    public GameObject npc_image;
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

    public void ImageUpdate()
    {
        if(this.npc_image != null)
        {
            this.npc_image.SetActive(true);
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
