using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool isTable;
    public int pieceCount = 0;
    [SerializeField] private GameObject player;
    public GameObject sketchbook;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private GameObject trigger;
    public Rigidbody2D rigid;

    private void Start()
    {
        rigid.mass = 1000;
    }


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

    public void AcitveBook()
    {
        sketchbook.SetActive(true);
    }

}
