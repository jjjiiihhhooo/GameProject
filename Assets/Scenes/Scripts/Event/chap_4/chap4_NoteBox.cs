using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_NoteBox : MonoBehaviour
{
    [HideInInspector] public chap4_NoteManager noteManager;
    public chap4_Note[] notes;

    protected bool canOutput; // �������� ��� ���� ����

    //---------------------------------------------------------------------------------Ȯ�ο�

    [HideInInspector] public bool isTrigger = false; // �÷��̾ ��Ҵ���
    [HideInInspector] public bool turnOn = true; // ������ DialogueBox ������Ʈ�� ������ ��� ���� ������Ʈ���� ����

    // ���� ��ȭ ���� ���� ����, ����, ���� ���¸� ��Ÿ��.
    [HideInInspector] public bool isLog;
    [HideInInspector] public bool isStarted; // �� ���̶� ��ȭ�� ȣ��Ǹ� ��� true
    [HideInInspector] public bool isEnd; // �� ���̶� ��ȭ�� ȣ��� ���� ����� ���� ������ ��� true
    [HideInInspector] public bool noMore;

    //---------------------------------------------------------------------------------Ȯ�ο�

    //public bool isTrigger = false; // �÷��̾ ��Ҵ���
    //public bool turnOn = true; // ������ DialogueBox ������Ʈ�� ������ ��� ���� ������Ʈ���� ����

    //// ���� ��ȭ ���� ���� ����, ����, ���� ���¸� ��Ÿ��.
    //public bool isLog;
    //public bool isStarted; // �� ���̶� ��ȭ�� ȣ��Ǹ� ��� true
    //public bool isEnd; // �� ���̶� ��ȭ�� ȣ��� ���� ����� ���� ������ ��� true
    //public bool noMore;

    //---------------------------------------------------------------------------------

    protected bool start;

    // ��ȭ �ݺ� ��� ����
    // breakConditions �� �Էµ� ���� �����Ǹ�, ���̻� ��ȭ/ ����â�� �ݺ��ؼ� ����� ����(isRepeat = false;)
    [Serializable]
    public struct BreakCondition
    {
        public int _index;
        public int _value;
        public bool _isClear;
    }
    [SerializeField]
    protected BreakCondition[] breakConditions;
    public bool isRepeat;
    protected int count = 0;
    protected int conLenth;

    public bool isContinue;

    protected virtual void Start()
    {
        noteManager = FindObjectOfType<chap4_NoteManager>();
        conLenth = breakConditions.Length;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = false;
        }
    }

    protected virtual void Update()
    {
        if (noteManager.isEnd && isStarted && !isEnd)
        {
            isEnd = true;
            noteManager.isEnd = false;
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
            count = 0;
            for (int i = 0; i < conLenth; i++)
            {
                if (noteManager.result[breakConditions[i]._index] == breakConditions[i]._value)
                {
                    breakConditions[i]._isClear = true;
                }
                if(breakConditions[i]._isClear == true)
                {
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
        if (!noMore)
        {
            noteManager.Talking = true;
            isTrigger = false;
            isStarted = true;
            noteManager.UpdateDialogue(notes);
            isLog = true;
        }
    }

    public bool GetBoolLog()
    {
        return noteManager.onDialogue;
    }
}

