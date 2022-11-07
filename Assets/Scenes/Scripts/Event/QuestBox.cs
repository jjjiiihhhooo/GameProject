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

    DialogueBox[] dialogueBoxes;

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
        public bool _isClear;
    }
    [SerializeField] LogCondition[] logConditions;


    private void Start()
    {
        dialogueBoxes = GetComponents<DialogueBox>();
    }

    private void Update()
    {
        CheckLC();
        CheckAC();
    }

    // LogCondition �˻�
    void CheckLC()
    {
        // �� logConditions[i] ���� �˻�
        for (int i = 0; i < logConditions.Length; i++)
        {
            if (dialogueBoxes[logConditions[i]._logCheck].isLog)
            {
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
        if(check >= 0)
        {
            if (check == 0)
                player.transform.position = mapTransform.position;
            else if (check == 1)
            {
                if (quest_image != null)
                    theQuestManager.ChangeItem(quest_item, quest_image);
                this.gameObject.SetActive(false);
            }
            else if (check == 4)
            {
                if (quest_image != null)
                    theQuestManager.ChangeItem(quest_item, quest_image);
                this.gameObject.SetActive(false);
            }
        }
    }
}
