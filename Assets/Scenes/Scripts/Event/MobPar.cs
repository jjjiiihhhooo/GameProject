using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPar : MonoBehaviour
{
    DialogueBox dialogueBox;
    //[SerializeField] private TestChat testChat;

    private void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
    }

    public void Chat()
    {
        dialogueBox.SetDialogue();
    }
}
