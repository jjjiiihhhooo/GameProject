using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_End_JY_0 : MonoBehaviour
{
    /* 
     * 퍼즐 4-1 준비
     */

    DialogueBox[] dialogueBoxes;
    int boxLength;
    float timer = 0;

    public GameObject Puzzle_4_1;

    void Start()
    {
        dialogueBoxes = GetComponents<DialogueBox>();
        boxLength = dialogueBoxes.Length;

    }

    void Update()
    {
        // 마지막 dialogueBoxes가 종료되었을 때
        if (dialogueBoxes[boxLength - 1].noMore) 
        {
            timer += Time.deltaTime;
            if(timer > 1.0f)
                Puzzle_4_1.SetActive(true);
        }
    }
}
