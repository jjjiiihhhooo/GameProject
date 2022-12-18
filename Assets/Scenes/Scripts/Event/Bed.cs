using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public bool isBed;
    public bool isActive = true;
    [SerializeField] private int check;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform room_transform;
    DialogueBox dialogueBox;
    PlayerMove PM;

    private void Start()
    {
        PM = player.GetComponent<PlayerMove>();
        dialogueBox = GetComponent<DialogueBox>();
        //player = GameObject.FindWithTag("Player");
        isActive = true;
        if (check == 1)
        {
            dialogueBox.isTrigger = true;
        }
    }

    private void Update()
    {
        if(this.isBed && dialogueBox.isLog)
        {
            if(check == 1)
            {
                isBed = false;
                PM.inEvent = false;
                player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0.0f));
            }
        }

    }

    public void BedTransform()
    {

        if (isActive)
        {
            PM = player.GetComponent<PlayerMove>();
            isBed = true;
            player.transform.position = room_transform.position;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            PM.inEvent = true;
            isActive = false;
        }
    }

    
   
}