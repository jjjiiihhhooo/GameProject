using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_GetNote : MonoBehaviour
{
    DialogueBox dialogueBox;
    [SerializeField] chap4_JY_playground jy_pg;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
    }

    
    void Update()
    {
        if(dialogueBox.noMore) 
        {
            jy_pg.count++;
            this.gameObject.SetActive(false);
        }
    }
}
