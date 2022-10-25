using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] private GameObject player; //이동 할 캐릭터
    [SerializeField] private Transform mapTransform;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject saveManager;
    [SerializeField] private GameObject saveChat;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.transform.position = mapTransform.position;
            fade.SetActive(true);
            mainCamera.GetComponent<CameraManager>().Bool();
            saveManager.GetComponent<SaveManager>().IsSave();
            saveChat.SetActive(true);
            Invoke("ChatActive", 2f);
        }
    }
    
    private void ChatActive()
    {
        saveChat.SetActive(false);
    }
}
