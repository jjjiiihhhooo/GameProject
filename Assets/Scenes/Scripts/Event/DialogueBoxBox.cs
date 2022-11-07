using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxBox : MonoBehaviour
{
    [HideInInspector] DialogueBox[] dialogueBoxes;
    bool goContinue;
    int logCheck = 0; // 현재 출력 가능 상태인 dialogueBox[]의 인덱스

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
        // dialogueBox[logCheck] 종료 이후 다음 출력 dialogueBox 조정
        if(logCheck >= 0)
        {
            if (dialogueBoxes[logCheck].noMore)
            {
                //Debug.Log($"name: {this.gameObject.name},log: {logCheck}, isEnd: {dialogueBoxes[logCheck].noMore}");
                goContinue = dialogueBoxes[logCheck].isContinue;

                dialogueBoxes[logCheck].turnOn = false;
                logCheck++;
                // 모든 dialogueBox가 종료되면 모두 비활성화
                if (logCheck >= dialogueBoxes.Length)
                    logCheck = -1;
                else if (logCheck < dialogueBoxes.Length)
                {
                    dialogueBoxes[logCheck].turnOn = true;
                    if (goContinue)
                        this.dialogueBoxes[logCheck].SetDialogue();
                    goContinue = false;
                }
            }
        }
    }
}
