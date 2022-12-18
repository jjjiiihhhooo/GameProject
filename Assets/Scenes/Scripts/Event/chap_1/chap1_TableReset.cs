using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap1_TableReset : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform one;
    [SerializeField] GameObject table;
    [SerializeField] Transform tableLoc;

    void Start()
    {
        //tableLoc = table.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "chap1_table")
        {
            player.transform.position = one.position;
            table.transform.position = tableLoc.position;
        }
    }
}
