using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueManager : MonoBehaviour
{
    /*
     * Dialogue[] 형식으로 받은 데이터를 출력한다.
     * UpdateDialogue(); -> StartDialogue(); -> ExitDialogue();
     * 순으로 진행된다.
     */

    public bool onDialogue = false;
    public bool isEnd = false;
    bool single;
    public bool GetKey = false; // 키를 입력 받을 수 있는 경우
    //====================================== 대화 관련 필드 ==============================================

    [SerializeField] public Text text;                             // sentences
    [SerializeField] public SpriteRenderer rendererLeft;           // Sprites
    [SerializeField] public SpriteRenderer rendererRight;          // Sprites

    [SerializeField] public Animator aniLeft;                      // 좌측 스프라이트
    [SerializeField] public Animator aniRight;                     // 우측 스프라이트
    [SerializeField] public Animator aniTopPanel;
    [SerializeField] public Animator aniBotPanel;

    List<string> sentenceList;
    List<Sprite> spriteList;
    int[] CharNum;

    public bool Talking = false; // 대화중
    int logLength = 0; // 총 대화 길이
    int logCount = 0; // 현재 대화 진행도

    //====================================== 선택 관련 필드 ==============================================

    [SerializeField] Animator aniAnswer;
    [SerializeField] Text answer_Text0;
    [SerializeField] Text answer_Text1;
    [SerializeField] GameObject[] answer_Panel;

    List<string> answerList_0;
    List<string> answerList_1;
    List<bool> isChoice;

    public bool Choosing = false;
    int answerLength = 2;
    int answerCount;
    public int[] result; // 결과값 저장. 초기값은 -1이므로 해당 값이 출력된다면, 선택 결과값이 없는 것이다.
    bool[] _choice;

    //====================================================================================================

    void Start()
    {
        text.text = "";
        answer_Text0.text = "";
        answer_Text1.text = "";

        sentenceList = new List<string>();
        answerList_0 = new List<string>();
        answerList_1 = new List<string>();
        spriteList = new List<Sprite>();
        isChoice = new List<bool>();
    }

    void Update()
    {
        onDialogue = Talking || Choosing ? true : false;

        // 게임 오브젝트에서 Talking을 활성화 시켜줄 것
        // 대화 중일 때
        if (logCount != 0 && Talking && !Choosing && GetKey && Input.GetKeyDown(KeyCode.Space))
        {

            GetKey = false;
            // 불러올 dialogue가 없을 때
            if (logCount >= logLength)
            {
                StopAllCoroutines();
                ExitDialogue();
            }
            else
            {
                StartCoroutine(this.StartDialogue());
            }
        }
        // 선택 중일 때
        else if (logCount != 0 && Talking && Choosing && GetKey)
        {
            // 선택지 이동
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (answerCount > 0)
                    answerCount--;
                else
                    answerCount = answerLength - 1;
                Chosen();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (answerCount < answerLength - 1)
                    answerCount++;
                else
                    answerCount = 0;
                Chosen();
            }
            // 선택
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GetKey = false;
                Choosing = false;
                aniAnswer.SetBool("Appear", false);

                text.text = "";
                answer_Text0.text = "";
                answer_Text1.text = "";

                // result[n]에 결과값 저장. ( n = 해당 질문이 있던 dialogue 인덱스)
                result[logCount - 1] = answerCount;

                if (logCount >= logLength)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                // 대화 진행
                else
                {
                    StartCoroutine(this.StartDialogue());
                }
            }
        }
    }

    // 초기 호출
    public void UpdateDialogue(Dialogue[] _dialogue)
    {
        single = true;
        logLength = _dialogue.Length;

        CharNum = new int[logLength];
        result = new int[logLength];
        _choice = new bool[logLength];

        for (int i = 0; i < logLength; i++)
        {
            sentenceList.Add(_dialogue[i].sentences);
            spriteList.Add(_dialogue[i].charSprites);
            CharNum[i] = _dialogue[i].charNum;
            answerList_0.Add(_dialogue[i].answers_0);
            answerList_1.Add(_dialogue[i].answers_1);
            isChoice.Add(_dialogue[i].choice);
            _choice[i] = _dialogue[i].choice;
            if (single)
                result[i] = -1;
        }
        single = false;
        StartCoroutine(StartDialogue());
    }

    // 대화 호출
    IEnumerator StartDialogue()
    {
        if (!isChoice[logCount]) text.text = "";
        // 첫 대화
        if (logCount == 0 && !isChoice[0])
        {
            // 상/하단 패널 생성
            aniTopPanel.SetBool("Appear", true);
            aniBotPanel.SetBool("Appear", true);
            aniAnswer.SetInteger("Chosen", 0);
            yield return new WaitForSeconds(0.25f);
        }
        // 첫 대화가 선택일 경우
        else if (isChoice[0])
        {
            Debug.Log("질문(대화)를 먼저 입력하시오.");
            Talking = Choosing = false;
            StopCoroutine(this.StartDialogue());
        }

        // 첫 번째 이후 대화
        // 화자에 따라 다른 스프라이트가 어두워짐
        if (logCount > 0 && !(logCount >= logLength))
        {
            // 화자가 변경되었을 때
            if (CharNum[logCount] != CharNum[logCount - 1])
            {
                // 화자가 하나일 때
                if (CharNum[logCount] == 0)
                {
                    // 지연이 등장한 상태라면 어두워짐
                    aniRight.SetBool("GetDark", aniRight.GetBool("Appear") ? true : false);
                }
                // 화자가 지연일 때
                else if (CharNum[logCount] == 1)
                {
                    // 하나가 등장한 상태라면 어두워짐
                    aniLeft.SetBool("GetDark", aniLeft.GetBool("Appear") ? true : false);
                }
                else
                {
                    aniRight.SetBool("GetDark", true);
                    aniLeft.SetBool("GetDark", true);
                }
            }
        }

        // 대화일 경우
        if (!isChoice[logCount] && !(logCount >= logLength))
        {
            // 첫 번째 이후 대화
            if (logCount > 0)
            {
                // 화자가 변경되었을 때
                if (CharNum[logCount] != CharNum[logCount - 1])
                {
                    // 화자가 하나일 때
                    if (CharNum[logCount] == 0)
                    {
                        // 지연이 등장한 상태라면 어두워짐
                        aniRight.SetBool("GetDark", aniRight.GetBool("Appear") ? true : false);
                    }
                    // 화자가 지연일 때
                    else if (CharNum[logCount] == 1)
                    {
                        // 하나가 등장한 상태라면 어두워짐
                        aniLeft.SetBool("GetDark", aniLeft.GetBool("Appear") ? true : false);
                    }
                }
            }

            // 화자가 하나일 때
            if (!(logCount >= logLength))
            {
                if (CharNum[logCount] == 0)
                {
                    if (aniLeft.GetBool("GetDark"))
                        aniLeft.SetBool("GetDark", false);
                    rendererLeft.GetComponent<SpriteRenderer>().sprite = spriteList[logCount];
                    if (!aniLeft.GetBool("Appear"))
                        aniLeft.SetBool("Appear", true);
                    yield return new WaitForSeconds(0.25f);
                }
                // 화자가 지연일 때
                else if (CharNum[logCount] == 1)
                {
                    if (aniRight.GetBool("GetDark"))
                        aniRight.SetBool("GetDark", false);
                    rendererRight.GetComponent<SpriteRenderer>().sprite = spriteList[logCount];
                    if (!aniRight.GetBool("Appear"))
                        aniRight.SetBool("Appear", true);
                    yield return new WaitForSeconds(0.25f);
                }
                // 대화 내용 한글자씩 출력
                for (int i = 0; i < sentenceList[logCount].Length; i++)
                {
                    text.text += sentenceList[logCount][i];
                    yield return new WaitForSeconds(0.01f);
                }

            }
        }
        // 선택일 경우
        else if (isChoice[logCount] && !(logCount >= logLength))
        {
            answerCount = 0;
            result[logCount] = -1;
            // answer 패널 생성
            aniAnswer.SetBool("Appear", true);
            yield return new WaitForSeconds(0.25f);

            // answer 출력
            for (int i = 0; i < answerList_0[logCount].Length; i++)
            {
                answer_Text0.text += answerList_0[logCount][i];
                yield return new WaitForSeconds(0.01f);
            }
            for (int i = 0; i < answerList_1[logCount].Length; i++)
            {
                answer_Text1.text += answerList_1[logCount][i];
                yield return new WaitForSeconds(0.01f);
            }
            Chosen();
            Choosing = true;
        }
        logCount++;
        GetKey = true;
    }

    // 대화 탈출
    public void ExitDialogue()
    {
        text.text = "";
        answer_Text0.text = "";
        answer_Text1.text = "";
        logCount = 0;
        CharNum = Enumerable.Repeat<int>(0, CharNum.Length).ToArray<int>();
        sentenceList.Clear();
        spriteList.Clear();
        answerList_0.Clear();
        answerList_1.Clear();
        isChoice.Clear();
        aniLeft.SetBool("Appear", false);
        aniRight.SetBool("Appear", false);
        aniTopPanel.SetBool("Appear", false);
        aniBotPanel.SetBool("Appear", false);
        aniAnswer.SetBool("Appear", false);
        Talking = false;
        isEnd = true;
    }

    void Chosen()
    {
        // 선택되지 않은 패널의 색을 연하게 함
        aniAnswer.SetInteger("Chosen", answerCount);
    }

    public bool[] GetIsChoice(int length)
    {
        bool[] _BoolArray = new bool[length];
        for (int i = 0; i < length; i++)
        {
            _BoolArray[i] = _choice[i];
        }
        return _BoolArray;
    }

    public void StartLog()
    {
        StartCoroutine(StartDialogue());
    }
}
