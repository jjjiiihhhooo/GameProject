using System.Collections;
using System.Threading;
using UnityEngine;
using System;
using static DialogueBox;
using Unity.VisualScripting;

public class DialogueBox : MonoBehaviour
{
    [HideInInspector] public DialogueManager dialogueManager;
    public Dialogue[] dialogues;

    bool canOutput; // �������� ��� ���� ����
    //[HideInInspector] public bool isTrigger = false; // �÷��̾ ��Ҵ���
    //[HideInInspector] public bool turnOn = true; // ������ DialogueBox ������Ʈ�� ������ ��� ���� ������Ʈ���� ����

    //// ���� ��ȭ ���� ���� ����, ����, ���� ���¸� ��Ÿ��.
    //[HideInInspector] public bool isLog;
    //[HideInInspector] public bool isStarted; // �� ���̶� ��ȭ�� ȣ��Ǹ� ��� true
    //[HideInInspector] public bool isEnd; // �� ���̶� ��ȭ�� ȣ��� ���� ����� ���� ������ ��� true
    //[HideInInspector] public bool noMore;

    //---------------------------------------------------------------------------------Ȯ�ο�

    public bool isTrigger = false; // �÷��̾ ��Ҵ���
    public bool turnOn = true; // ������ DialogueBox ������Ʈ�� ������ ��� ���� ������Ʈ���� ����

    // ���� ��ȭ ���� ���� ����, ����, ���� ���¸� ��Ÿ��.
    public bool isLog;
    public bool isStarted; // �� ���̶� ��ȭ�� ȣ��Ǹ� ��� true
    public bool isEnd; // �� ���̶� ��ȭ�� ȣ��� ���� ����� ���� ������ ��� true
    public bool noMore;
    bool start;

    // ��ȭ �ݺ� ��� ����
    // breakConditions �� �Էµ� ���� �����Ǹ�, ���̻� ��ȭ/ ����â�� �ݺ��ؼ� ����� ����(isRepeat = false;)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
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

        // ��ȭ/����â ���
        if (canOutput && Input.GetKeyDown(KeyCode.Space))
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
            isLog = true;
        }
    }

    public bool GetBoolLog()
    {
        return dialogueManager.onDialogue;
    }
}
