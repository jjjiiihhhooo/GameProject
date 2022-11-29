using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxRan : MonoBehaviour
{
    [HideInInspector] DialogueBox[] dialogueBoxes;
    int logCheck = 0;
    int preCheck = -1;

    void Start()
    {
        dialogueBoxes = GetComponents<DialogueBox>();
        // dialogueBox[logCheck]을 제외하고 비활성화
        for (int i = 0; i < dialogueBoxes.Length; i++)
        {
            if (i != logCheck)
                dialogueBoxes[i].turnOn = false;
        }
    }

    void Update()
    {
        if (dialogueBoxes[logCheck].noMore)
        {
            dialogueBoxes[logCheck].isStarted = false;
            dialogueBoxes[logCheck].isEnd = false;
            dialogueBoxes[logCheck].noMore = false;

            // 이미 실행한 대화를 다음 대화에서는 제외
            preCheck = logCheck;
            while(preCheck == logCheck)
                logCheck = Random.Range(0, dialogueBoxes.Length);

            for (int i = 0; i < dialogueBoxes.Length; i++)
            {
                if (i != logCheck)
                    dialogueBoxes[i].turnOn = false;
                else if (i == logCheck)
                    dialogueBoxes[i].turnOn = true;
            }
        }
    }
}
