using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool isTable;
    [SerializeField] private GameObject player;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private GameObject trigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Trigger")
        {
            isTable = true;
            box.isTrigger = true;
            trigger.GetComponent<TestChat>().isTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Trigger")
        {
            isTable = false;
        }
    }


}
