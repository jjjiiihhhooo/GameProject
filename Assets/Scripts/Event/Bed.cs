using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    public bool isBed;
    [SerializeField] private int check;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform room_transform;
    
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
        isBed = true;
        player.transform.position = room_transform.position;
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
        player.GetComponent<PlayerMove>().isMove = false;
    }

    
   
}