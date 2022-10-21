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

    private void Awake()
    {
        isActive = true;
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
                player.transform.position = room_transform.position;
            }
        }

    }

    public void BedTransform()
    {
        if(isActive)
        {
            isBed = true;
            player.transform.position = room_transform.position;
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            player.GetComponent<PlayerMove>().isMove = false;
            isActive = false;
        }
    }

    
   
}