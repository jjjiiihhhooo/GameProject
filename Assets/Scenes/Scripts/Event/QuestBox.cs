using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.ConstrainedExecution;

public class QuestBox : MonoBehaviour
{
    [SerializeField] private QuestManager theQuestManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform mapTransform;
    [SerializeField] private Sprite quest_image;
    [SerializeField] private string quest_item;
    [SerializeField] FadeInOut fade;
    [SerializeField] Act2 act2;
    [SerializeField] private Transform mapTransform2;
    [SerializeField] private CameraManager main_camera;

    DialogueBox[] dialogueBoxes;
    AudioSource audioSource;
    chap4_NoteLog noteLog;

    [SerializeField] bool Get;
    
    // �Ʒ��� LogCondition ��, LogCondition[ConNum]�� ��ġ�ϸ� Action(ActNum) ����
    [Serializable]
    public struct ActionCondition
    {
        public int[] ConNum;
        public int ActNum;
    }
    [SerializeField] ActionCondition[] actionConditoins;

    // �亯 ���� ���� ����ü
    [Serializable]
    public struct LogCondition
    {
        // ����: dialogueBox[_logCheck]�� _index ��° ���� �亯�� _value �� ���
        public int _logCheck;
        public int _index;
        public int _value;
        public bool _isNoMore;
        public bool _isClear;
    }
    [SerializeField] LogCondition[] logConditions;


    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueBoxes = GetComponents<DialogueBox>();
        if (theQuestManager == null)
            theQuestManager = GameObject.FindWithTag("QuestManager").GetComponent<QuestManager>();
        if (Get) Action(1);
    }

    private void Update()
    {
        CheckLC();
        CheckAC();
    }

    // LogCondition �˻�
    void CheckLC()
    {
        if(logConditions.Length>0)
        // �� logConditions[i] ���� �˻�
        for (int i = 0; i < logConditions.Length; i++)
        {
            if (dialogueBoxes[logConditions[i]._logCheck].isLog)
            {
                if (logConditions[i]._isNoMore && dialogueBoxes[logConditions[i]._logCheck].noMore)
                    logConditions[i]._isClear = true; // logConditions[i] ���� �޼�!
                if (dialogueBoxes[logConditions[i]._logCheck].dialogueManager.result[logConditions[i]._index] == logConditions[i]._value)
                    logConditions[i]._isClear = true; // logConditions[i] ���� �޼�!
            }
        }
    }

    // ActionCondition �˻�
    void CheckAC()
    {
        int count = 0;
        for (int i = 0; i < actionConditoins.Length; i++)
        {
            for (int j = 0; j < actionConditoins[i].ConNum.Length; j++)
            {
                if (logConditions[actionConditoins[i].ConNum[j]]._isClear == true)
                {
                    count++;
                    if (count == actionConditoins[i].ConNum.Length)
                    {
                        Action(actionConditoins[i].ActNum);
                        actionConditoins[i].ActNum = -1;
                    }
                }
            }
        }
    }

    void Action(int check)
    {
        if (check >= 0)
        {
            switch (check)
            {
                case 0:
                    if (check == 0)
                        player.transform.position = mapTransform.position;
                    break;
                case 1:
                    if(quest_item != null&& quest_image != null)
                    if(audioSource) audioSource.Play();
                    theQuestManager.ChangeItem(quest_item, quest_image);
                    break;
                case 2:
                    act2.StartAct2();
                    break;
                case 4:
                    if (quest_image != null)
                        theQuestManager.ChangeItem(quest_item, quest_image);
                    this.gameObject.SetActive(false);

                    break;
            }
        }
    }
}
