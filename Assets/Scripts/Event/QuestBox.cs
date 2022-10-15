using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBox : MonoBehaviour
{
    [SerializeField] private TestChat testChat;
    [SerializeField] private ChoiceEvent choice;
    [SerializeField] private bool isTest;
    public bool isTest2 = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTest = true;
        }
    }

    private void Update()
    {
        if(((choice != null && choice.isTrigger) || choice == null ) && isTest2 && isTest && Input.GetKeyDown(KeyCode.Space))
        {
            testChat.isTarget = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTest = false;
        }
    }
}
