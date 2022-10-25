using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveCheck : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveChat;
    [SerializeField] private string text;
    [SerializeField] private int checkCount;
    private bool isStart;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            if(!isStart && checkCount == 1)
            {
                isStart = true;
                saveManager.IsSave();
                saveChat.GetComponent<Text>().text = text;
                saveChat.SetActive(true);
                Invoke("SaveChat", 2f);
            }
            else if(!isStart)
            {
                isStart = true;
                saveChat.GetComponent<Text>().text = text;
                saveChat.SetActive(true);
                Invoke("SaveChat", 2f);
            }
        }
    }

    private void SaveChat()
    {
        saveChat.SetActive(false);
    }
}
