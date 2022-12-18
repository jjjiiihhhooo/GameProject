using System.Collections;
using System.Threading;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class DialogueBox : MonoBehaviour
{
    [HideInInspector] public DialogueManager dialogueManager;
    public Dialogue[] dialogues;

    bool canOutput; // 종합적인 출력 가능 여부

    //---------------------------------------------------------------------------------확인용

    //[HideInInspector] public bool isTrigger = false; // 플레이어가 닿았는지
    //[HideInInspector] public bool turnOn = true; // 복수의 DialogueBox 컴포넌트가 존재할 경우 게임 오브젝트에서 제어

    //// 각각 대화 현재 진행 여부, 시작, 종료 상태를 나타냄.
    //[HideInInspector] public bool isLog;
    //[HideInInspector] public bool isStarted; // 한 번이라도 대화가 호출되면 계속 true
    //[HideInInspector] public bool isEnd; // 한 번이라도 대화가 호출된 이후 종료된 적이 있으면 계속 true
    //[HideInInspector] public bool noMore;

    //---------------------------------------------------------------------------------확인용

    public bool isTrigger = false; // 플레이어가 닿았는지
    public bool turnOn = true; // 복수의 DialogueBox 컴포넌트가 존재할 경우 게임 오브젝트에서 제어

    // 각각 대화 현재 진행 여부, 시작, 종료 상태를 나타냄.
    public bool isLog;
    public bool isStarted; // 한 번이라도 대화가 호출되면 계속 true
    public bool isEnd; // 한 번이라도 대화가 호출된 이후 종료된 적이 있으면 계속 true
    public bool noMore;

    //---------------------------------------------------------------------------------

    bool start;

    // 대화 반복 출력 여부
    // breakConditions 에 입력된 값이 충족되면, 더이상 대화/ 선택창을 반복해서 띄우지 않음(isRepeat = false;)
    [Serializable]
    public struct BreakCondition
    {
        public int _index;
        public int _value;
        public bool _isClear;
    }
    [SerializeField] BreakCondition[] breakConditions;
    public bool isRepeat;
    int count = 0;
    int conLenth;

    public bool isContinue;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        conLenth = breakConditions.Length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ray")
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "ray")
        {
            isTrigger = false;
        }
    }

    void Update()
    {
        if (dialogueManager.isEnd && isStarted && !isEnd)
        {
            isEnd = true;
            dialogueManager.isEnd = false;
        }

        if (isLog && !GetBoolLog())
        {
            isLog = false;
        }
        noMore = !isRepeat && isEnd ? true : false;

        canOutput = isTrigger && turnOn ? true : false;

        // 대화/선택창 출력
        if (canOutput && !dialogueManager.Talking && Input.GetKeyDown(KeyCode.Space))
        {
            SetDialogue();
        }

        if (this.isLog) start = true;

        if (start && this.isRepeat)
        {
            for (int i = 0; i < conLenth; i++)
            {
                if (dialogueManager.result[breakConditions[i]._index] == breakConditions[i]._value)
                {
                    breakConditions[i]._isClear = true;
                    count++;
                    if (count == conLenth)
                    {
                        isRepeat = false;
                        count = 0;
                    }
                }
            }
        }
    }

    public void SetDialogue()
    {
        if(!noMore)
        {
            dialogueManager.Talking = true;
            isTrigger = false;
            isStarted = true;
            dialogueManager.UpdateDialogue(dialogues);
            Debug.Log(gameObject.name);
            isLog = true;
        }
    }

    public bool GetBoolLog()
    {
        return dialogueManager.onDialogue;
    }
}
