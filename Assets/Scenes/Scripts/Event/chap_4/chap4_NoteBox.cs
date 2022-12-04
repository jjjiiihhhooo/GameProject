using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_NoteBox : MonoBehaviour
{
    [HideInInspector] public chap4_NoteManager noteManager;
    public chap4_Note[] notes;
    SceneChanger sceneChanger;

    bool canOutput; // �������� ��� ���� ����

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
    [SerializeField]
    BreakCondition[] breakConditions;
    public bool isRepeat;
    int count = 0;
    int conLenth;

    public bool isContinue;

    void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
        noteManager = FindObjectOfType<chap4_NoteManager>();
        conLenth = breakConditions.Length;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTrigger = false;
        }
    }

    void Update()
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
                        sceneChanger.chap4JY = true;
                        count = 0;
                    }
                    else if (noteManager.tryNum > 2 && isLog)
                    {
                        turnOn = false;
                        noMore= true;
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

