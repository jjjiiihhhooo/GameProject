using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public bool isBed;
    [SerializeField] private bool isBed2;
    [SerializeField] private bool isSleep = false;
    [SerializeField] private int check;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bed_box;
    [SerializeField] private GameObject bed_box2;
    [SerializeField] private Transform room_transform;
    private TestChat chat;
    public Rigidbody2D rigid;
    public BoxCollider2D box;
    

    private void Awake()
    {
        chat = GetComponent<TestChat>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isSleep = true;
        }
    }
    
    private void Update()
    {
        if(this.isBed && Input.GetKeyDown(KeyCode.Space))
        {
            if(check == 1)
            {
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                isBed = false;
                player.GetComponent<PlayerMove>().isMove = true;
                chat.isTarget = false;
                player.transform.position = room_transform.position;
            }
        }

    }

    public void BedTransform()
    {
        isBed = true;
        isBed2 = true;
        rigid.mass = 1000;
        box.isTrigger = true;
        player.transform.position = new Vector2(-5.7f, -3.5f);
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        player.GetComponent<PlayerMove>().isMove = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(check == 1)
            {
                isBed = false;
                box.isTrigger = false;
                bed_box.GetComponent<ChoiceEvent>().isTrigger = true;
            }
            else if(check == 2)
            {
                isBed = false;
            }
        }
    }

    
   
}