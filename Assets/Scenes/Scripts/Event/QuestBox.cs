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
    [SerializeField] private GameObject fade;
    [SerializeField] private Transform mapTransform2;
    [SerializeField] private CameraManager main_camera;

    DialogueBox[] dialogueBoxes;

    // 아래의 LogCondition 중, LogCondition[ConNum]이 일치하면 Action(ActNum) 실행
    [Serializable]
    public struct ActionCondition
    {
        public int[] ConNum;
        public int ActNum;
    }
    [SerializeField] ActionCondition[] actionConditoins;

    // 답변 조건 관련 구조체
    [Serializable]
    public struct LogCondition
    {
        // 조건: dialogueBox[_logCheck]의 _index 번째 문항 답변이 _value 일 경우
        public int _logCheck;
        public int _index;
        public int _value;
        public bool _isNoMore;
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

    // LogCondition 검사
    void CheckLC()
    {
        // 각 logConditions[i] 조건 검사
        for (int i = 0; i < logConditions.Length; i++)
        {
            if (dialogueBoxes[logConditions[i]._logCheck].isLog)
            {
                if(logConditions[i]._isNoMore && dialogueBoxes[logConditions[i]._logCheck].noMore)
                    logConditions[i]._isClear = true; // logConditions[i] 조건 달성!
                if (dialogueBoxes[logConditions[i]._logCheck].dialogueManager.result[logConditions[i]._index] == logConditions[i]._value)
                    logConditions[i]._isClear = true; // logConditions[i] 조건 달성!
            }
        }
    }

    // ActionCondition 검사
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
                    if (quest_image != null)
                        theQuestManager.ChangeItem(quest_item, quest_image);
                    break;
                case 2:
                    fade.SetActive(true);
                    player.transform.position = mapTransform2.position;
                    main_camera.Bool();
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
