using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DialogueBox : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    DialogueManager dialogueManager;
    public GameObject Object;
    public Dialogue[] dialogues;

    bool isTrigger = false;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }
    
    void Update()
    {
        if (isTrigger && Input.GetKeyDown(KeyCode.Space))
        {
            dialogueManager.Talking = true;
            isTrigger = false;
            dialogueManager.UpdateDialogue(dialogues);
        }
    }
}
