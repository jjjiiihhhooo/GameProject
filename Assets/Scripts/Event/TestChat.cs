using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestChat : MonoBehaviour
{
    public GameObject npc_image;
    private ChatManager theChatManager;
    public Chat chat;
    public int check;
    public int[] animCount;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject fade;
    [SerializeField] private Transform mapTransform;
    [SerializeField] private Transform mapTransform2;
    [SerializeField] private CameraManager main_camera;

    public bool isTrigger = false;
    public bool isTarget = true;
    public bool isStart;
    public bool isQuest;

    void Start()
    {
        theChatManager = FindObjectOfType<ChatManager>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(check <= 0)
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
            if(isQuest)
            {
                Chat(0.2f);
            }
            else
            {
                ChatUpdate();
            }
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


    public void Chat(float _float = 0)
    {
        Invoke("ChatUpdate", _float);
    }

    private void ChatUpdate()
    {
        theChatManager.ShowChat(chat, animCount);
    }

    public void MapChange()
    {
        fade.SetActive(true);
        main_camera.Transform(mapTransform);
        Invoke("MapChange_2", 1.5f);

    }

    private void MapChange_2()
    {
        fade.SetActive(true);
        player.transform.position = mapTransform2.position;
        main_camera.Bool();
    }
}
