using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxBox : MonoBehaviour
{
    [HideInInspector] DialogueBox[] dialogueBoxes;
    bool goContinue;
    int logCheck = 0; // ���� ��� ���� ������ dialogueBox[]�� �ε���

    void Start()
    {
        dialogueBoxes = GetComponents<DialogueBox>();
        // dialogueBox[logCheck]�� �����ϰ� ��Ȱ��ȭ
        for (int i = 0; i < dialogueBoxes.Length; i++)
        {
            if (i != logCheck)
                dialogueBoxes[i].turnOn = false;
        }
    }

    void Update()
    {
        // dialogueBox[logCheck] ���� ���� ���� ��� dialogueBox ����
        if(logCheck >= 0)
        {
            if (dialogueBoxes[logCheck].noMore)
            {
                //Debug.Log($"name: {this.gameObject.name},log: {logCheck}, isEnd: {dialogueBoxes[logCheck].noMore}");
                goContinue = dialogueBoxes[logCheck].isContinue;

                dialogueBoxes[logCheck].turnOn = false;
                logCheck++;
                // ��� dialogueBox�� ����Ǹ� ��� ��Ȱ��ȭ
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
