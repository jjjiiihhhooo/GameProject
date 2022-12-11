using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_N_Coll : MonoBehaviour
{
    [SerializeField] GameObject end;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            end.SetActive(true);
        }
    }
}
