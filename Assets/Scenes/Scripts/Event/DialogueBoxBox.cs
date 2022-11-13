using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxBox : MonoBehaviour
{
    [HideInInspector] DialogueBox[] dialogueBoxes;
    bool goContinue;
    int logCheck = 0; // ���� ��� ���� ������ dialogueBox[]�� �ε���
    int preLogCount = 0;
    int preLogLength = 0;
    bool singleCall = true;
    bool[] isChoiceArray;

    [Serializable]
    public struct _GoTo
    {
        // ����: dialogueBox[_logCheck]�� _index ��° ���� �亯�� _value �� ���
        public int _logCheck;
        public int _index;
        public int _value;
        public int _goTo;
        public bool _isContinue;
        public bool _isClear;
    }
    [SerializeField] _GoTo[] goTo;

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
        if (preLogCount < dialogueBoxes.Length && logCheck != -1)
        {
            preLogLength = dialogueBoxes[preLogCount].dialogues.Length;
        }

        // dialogueBox[logCheck] ���� ���� ���� ��� dialogueBox ����
        if (logCheck >= 0)
        {
            if (dialogueBoxes[preLogCount].isLog)
            {
                CheckConditions();
                isChoiceArray = dialogueBoxes[preLogCount].dialogueManager.GetIsChoice(preLogLength);
            }
            if (dialogueBoxes[preLogCount].noMore)
            {
                if (singleCall)
                {
                    singleCall = false;
                    StartCoroutine(CheckTurnOn());
                }
            }
        }
    }

    // goTo ���� �˻�
    void CheckConditions()
    {
        for (int i = 0; i < goTo.Length; i++)
        {
            if (goTo[i]._logCheck == preLogCount && dialogueBoxes[goTo[i]._logCheck].dialogueManager.result[goTo[i]._index] == goTo[i]._value)
            {
                goTo[i]._isClear = true; // goTo[i] ���� �޼�!
                dialogueBoxes[goTo[i]._logCheck].dialogueManager.result[goTo[i]._index] = -1; // �ʱ�ȭ
            }
        }
    }

    void CheckGoTo()
    {
        int i;
        bool check = false;
        for (i = 0; i < goTo.Length; i++)
        {
            if (goTo[i]._logCheck == preLogCount)
            {
                if (goTo[i]._isClear)
                {
                    // ���� �������� ���� logCount ���
                    check = true;
                    logCheck = goTo[i]._goTo;
                    goTo[i]._isClear = false;
                    // ���� ���� ���� ���� ��ȭ�� ��� ȣ���� ���. ���� Dialogue�� isContinue�� �����̴�.
                    goContinue = goTo[i]._isContinue;
                    break;
                }
            }
        }
        // goTo ������ ���� ���
        if (!check || goTo.Length == 0)
        {
            logCheck++;
        }
    }

    IEnumerator CheckTurnOn()
    {
        preLogCount = logCheck;

        dialogueBoxes[logCheck].turnOn = false;

        // _GoTo �˻�. ���� logCheck�� ���� �Ǿ�����.
        CheckGoTo();

        if (!goContinue)
        {
            goContinue = dialogueBoxes[preLogCount].isContinue;
        }

        // ��� dialogueBox�� ����Ǹ� ��� ��Ȱ��ȭ
        if (logCheck >= dialogueBoxes.Length)
        {
            logCheck = -1;
        }
        else if (logCheck < dialogueBoxes.Length)
        {
            // ������ Dialogue�� ��ȭ�� ���
            if (!isChoiceArray[preLogLength - 1])
            {
                // ��ȭ�� ����� ������ ���� ��ȭ�� �̷��.
                while (true)
                {
                    if (!dialogueBoxes[preLogCount].isLog)
                    {
                        dialogueBoxes[logCheck].turnOn = true;
                        break;
                    }
                    yield return null;
                }
            }
            // ������ Dialogue�� ������ ���
            else if (isChoiceArray[preLogLength - 1])
                dialogueBoxes[logCheck].turnOn = true;

            //goContinue ���ο� ���� ������ȭ ��� ȣ�� ���ΰ� ��������.
            if (dialogueBoxes[logCheck].turnOn && goContinue)
            {
                this.dialogueBoxes[logCheck].SetDialogue();
            }
            goContinue = false;
        }

        // ���� ��� isRepeat�� üũ�ϸ� �Ƿ���
        // �Ƿ� logCheck�� �۾��� ��� �װ��� ���¸� �ѹ��Ѵ�.
        
        if (preLogCount > logCheck && logCheck >= 0)
        {
            for (; preLogCount >= logCheck; preLogCount--)
            {
                dialogueBoxes[preLogCount].isStarted = false;
                dialogueBoxes[preLogCount].isEnd = false;
                dialogueBoxes[preLogCount].noMore = false;
            }

        }
        preLogCount = logCheck;

        singleCall = true;
    }
}
