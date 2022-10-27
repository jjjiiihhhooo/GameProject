using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveCheck : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveChat;
    [SerializeField] private TestChat testChat;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject blackDisplay;
    [SerializeField] private string text;
    [SerializeField] private int checkCount;
    private bool isStart;

    private void Awake()
    {
        testChat = GetComponent<TestChat>();

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            

            if (!isStart && checkCount == 1)
            {
                isStart = true;
                saveManager.IsSave();
                saveChat.GetComponent<Text>().text = text;
                Invoke("SaveChatTrue", 2f);

                if (blackDisplay != null)
                {
                    blackDisplay.SetActive(true);
                    Chat();
                    Invoke("DisplayFalse", 4f);
                }

            }
            else if(!isStart)
            {
                isStart = true;
                saveChat.GetComponent<Text>().text = text;
                Invoke("SaveChatTrue", 2f);
            }
        }
    }

    private void Chat()
    {
        testChat.isTarget = false;
        testChat.Chat();
    }

    private void DisplayFalse()
    {
        fade.SetActive(true);
        blackDisplay.SetActive(false);
    }

    private void SaveChatFalse()
    {
        saveChat.SetActive(false);
    }

    private void SaveChatTrue()
    {
        saveChat.SetActive(true);
        Invoke("SaveChatFalse", 2f);
    }
}
