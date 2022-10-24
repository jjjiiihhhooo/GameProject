using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveCheck : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveChat;
    private bool isStart;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            if(!isStart)
            {
                isStart = true;
                saveManager.IsSave();
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
