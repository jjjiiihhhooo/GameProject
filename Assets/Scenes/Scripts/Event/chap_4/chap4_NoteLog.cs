using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_NoteLog : MonoBehaviour
{
    QuestInventory QI;

    public Dialogue[] dialogues;

    bool isTrigger = false;

    private void Awake()
    {
        QI = FindObjectOfType<QuestInventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = false;
        }
    }

    void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            QI.dialogues = dialogues;
        }
    }
}
