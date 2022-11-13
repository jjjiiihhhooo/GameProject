using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxBox : MonoBehaviour
{
    [HideInInspector] DialogueBox[] dialogueBoxes;
    bool goContinue;
    int logCheck = 0; // 현재 출력 가능 상태인 dialogueBox[]의 인덱스
    int preLogCount = 0;
    int preLogLength = 0;
    bool singleCall = true;
    bool[] isChoiceArray;

    [Serializable]
    public struct _GoTo
    {
        // 조건: dialogueBox[_logCheck]의 _index 번째 문항 답변이 _value 일 경우
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
        // dialogueBox[logCheck]을 제외하고 비활성화
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

        // dialogueBox[logCheck] 종료 이후 다음 출력 dialogueBox 조정
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

    // goTo 조건 검사
    void CheckConditions()
    {
        for (int i = 0; i < goTo.Length; i++)
        {
            if (goTo[i]._logCheck == preLogCount && dialogueBoxes[goTo[i]._logCheck].dialogueManager.result[goTo[i]._index] == goTo[i]._value)
            {
                goTo[i]._isClear = true; // goTo[i] 조건 달성!
                dialogueBoxes[goTo[i]._logCheck].dialogueManager.result[goTo[i]._index] = -1; // 초기화
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
                    // 조건 만족으로 인한 logCount 상승
                    check = true;
                    logCheck = goTo[i]._goTo;
                    goTo[i]._isClear = false;
                    // 조건 충족 이후 다음 대화를 즉시 호출할 경우. 기존 Dialogue의 isContinue는 별도이다.
                    goContinue = goTo[i]._isContinue;
                    break;
                }
            }
        }
        // goTo 조건이 없을 경우
        if (!check || goTo.Length == 0)
        {
            logCheck++;
        }
    }

    IEnumerator CheckTurnOn()
    {
        preLogCount = logCheck;

        dialogueBoxes[logCheck].turnOn = false;

        // _GoTo 검사. 이후 logCheck는 변동 되어있음.
        CheckGoTo();

        if (!goContinue)
        {
            goContinue = dialogueBoxes[preLogCount].isContinue;
        }

        // 모든 dialogueBox가 종료되면 모두 비활성화
        if (logCheck >= dialogueBoxes.Length)
        {
            logCheck = -1;
        }
        else if (logCheck < dialogueBoxes.Length)
        {
            // 마지막 Dialogue가 대화일 경우
            if (!isChoiceArray[preLogLength - 1])
            {
                // 대화가 종료될 때까지 다음 대화를 미룬다.
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
            // 마지막 Dialogue가 선택일 경우
            else if (isChoiceArray[preLogLength - 1])
                dialogueBoxes[logCheck].turnOn = true;

            //goContinue 여부에 따라 다음대화 즉시 호출 여부가 정해진다.
            if (dialogueBoxes[logCheck].turnOn && goContinue)
            {
                this.dialogueBoxes[logCheck].SetDialogue();
            }
            goContinue = false;
        }

        // 같을 경우 isRepeat을 체크하면 되려나
        // 되려 logCheck가 작아질 경우 그간의 상태를 롤백한다.
        
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
