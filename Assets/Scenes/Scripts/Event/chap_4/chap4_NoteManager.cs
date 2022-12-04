using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class chap4_NoteManager : MonoBehaviour
{
    DialogueManager dialogueManager;
    public bool onDialogue = false;
    public bool isEnd = false;
    bool single;
    bool GetKey = false; // 키를 입력 받을 수 있는 경우
    string chosenSTC;
    int preCount = 0;

    //====================================== 대화 관련 필드 ==============================================

    Text text;                             // sentences
    SpriteRenderer rendererLeft;           // Sprites
    SpriteRenderer rendererRight;          // Sprites

    Animator aniLeft;                      // 좌측 스프라이트
    Animator aniRight;                     // 우측 스프라이트
    Animator aniTopPanel;
    Animator aniBotPanel;

    List<string> sentenceList;
    List<Sprite> spriteList;
    int[] CharNum;

    public bool Talking = false; // 대화중
    int logLength = 0; // 총 대화 길이
    int logCount = 0; // 현재 대화 진행도

    //====================================== 선택 관련 필드 ==============================================

    [SerializeField] Animator aniNotes;
    //[SerializeField] GameObject[] note_Panel = new GameObject[6];

    string[] chosenSentence_0;
    string[] chosenSentence_1;
    string[] chosenSentence_2;
    string[] chosenSentence_3;
    string[] chosenSentence_4;
    string[] chosenSentence_5;

    List<bool> isNote;

    public bool Noting = false;
    int noteLength = 6;
    int noteCount;
    float aniNum = 0;
    bool[] givenNum = new bool[6];
    public int[] result; // 결과값 저장. 초기값은 -1이므로 해당 값이 출력된다면, 선택 결과값이 없는 것이다.
    bool[] note;

    //====================================================================================================

    void Start()
    {
        for(int i = 0; i < givenNum.Length; i++)
            givenNum[i] = false;

        sentenceList = new List<string>();
        spriteList = new List<Sprite>();
        isNote = new List<bool>();
        
        dialogueManager = FindObjectOfType<DialogueManager>();

        aniLeft = dialogueManager.aniLeft;
        aniRight = dialogueManager.aniRight;
        aniTopPanel = dialogueManager.aniTopPanel;
        aniBotPanel = dialogueManager.aniBotPanel;
        text = dialogueManager.text;

        text.text = "";
    }

    void Update()
    {
        onDialogue = Talking || Noting ? true : false;

        // 게임 오브젝트에서 Talking을 활성화 시켜줄 것
        // 대화 중일 때
        if (logCount != 0 && Talking && !Noting && GetKey && Input.GetKeyDown(KeyCode.Space))
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
                if (!isNote[logCount]) text.text = "";
                StartCoroutine(this.StartDialogue());
            }
        }
        // 선택 중일 때
        else if (logCount != 0 && Talking && Noting && GetKey)
        {
            // 선택지 이동
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (noteCount > 0)
                    noteCount--;
                else
                    noteCount = noteLength - 1;
                Chosen();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (noteCount < noteLength - 1)
                    noteCount++;
                else
                    noteCount = 0;
                Chosen();
            }
            // 선택
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GetKey = false;
                Noting = false;
                aniNotes.SetBool("isAppear", false);

                text.text = "";

                // result[n]에 결과값 저장. ( n = 해당 질문이 있던 dialogue 인덱스)
                result[logCount - 1] = noteCount;
                givenNum[noteCount] = true;

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
    public void UpdateDialogue(chap4_Note[] _notes)
    {
        single = true;
        logLength = _notes.Length;

        CharNum = new int[logLength];
        result = new int[logLength];
        note = new bool[logLength];

        for (int i = 0; i < logLength; i++)
        {
            sentenceList.Add(_notes[i].sentences);
            spriteList.Add(_notes[i].charSprites);
            CharNum[i] = _notes[i].charNum;
            isNote.Add(_notes[i].isNote);
            note[i] = _notes[i].isNote;
            if (single)
            {
                result[i] = -1;
                chosenSentence_0 = new string[logLength];
                chosenSentence_1 = new string[logLength];
                chosenSentence_2 = new string[logLength];
                chosenSentence_3 = new string[logLength];
                chosenSentence_4 = new string[logLength];
                chosenSentence_5 = new string[logLength];
            }

            chosenSentence_0[i] = _notes[i].NoteSTC0;
            chosenSentence_1[i] = _notes[i].NoteSTC1;
            chosenSentence_2[i] = _notes[i].NoteSTC2;
            chosenSentence_3[i] = _notes[i].NoteSTC3;
            chosenSentence_4[i] = _notes[i].NoteSTC4;
            chosenSentence_5[i] = _notes[i].NoteSTC5;
        }
        single = false;
        StartCoroutine(StartDialogue());
    }

    // 대화 호출
    IEnumerator StartDialogue()
    {
        // 첫 대화
        if (logCount == 0 && !isNote[0])
        {
            // 상/하단 패널 생성
            aniTopPanel.SetBool("Appear", true);
            aniBotPanel.SetBool("Appear", true);
            aniNotes.SetFloat("ChosenNum", 0);
            yield return new WaitForSeconds(0.25f);
        }
        // 첫 대화가 선택일 경우
        else if (isNote[0])
        {
            Debug.Log("질문(대화)를 먼저 입력하시오.");
            Talking = Noting = false;
            StopCoroutine(this.StartDialogue());
        }

        // 첫 번째 이후 대화
        // 화자에 따라 다른 스프라이트가 어두워짐
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
                else
                {
                    aniRight.SetBool("GetDark", true);
                    aniLeft.SetBool("GetDark", true);
                }
            }
        }

        // 대화일 경우
        if (!isNote[logCount])
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
        // 선택일 경우
        else if (isNote[logCount])
        {
            noteCount = 0;
            result[logCount] = -1;
            // answer 패널 생성
            aniNotes.SetBool("isAppear", true);
            yield return new WaitForSeconds(0.25f);

            // answer 출력
            //for (int i = 0; i < answerList_0[logCount].Length; i++)
            //{
            //    answer_Text0.text += answerList_0[logCount][i];
            //    yield return new WaitForSeconds(0.01f);
            //}
            //for (int i = 0; i < answerList_1[logCount].Length; i++)
            //{
            //    answer_Text1.text += answerList_1[logCount][i];
            //    yield return new WaitForSeconds(0.01f);
            //}
            preCount = logCount;
            Chosen();
            Noting = true;
        }
        logCount++;
        GetKey = true;
    }

    // 대화 탈출
    void ExitDialogue()
    {
        text.text = "";
        logCount = 0;
        CharNum = Enumerable.Repeat<int>(0, CharNum.Length).ToArray<int>();
        sentenceList.Clear();
        spriteList.Clear();
        isNote.Clear();
        aniLeft.SetBool("Appear", false);
        aniRight.SetBool("Appear", false);
        aniTopPanel.SetBool("Appear", false);
        aniBotPanel.SetBool("Appear", false);
        aniNotes.SetBool("isAppear", false);
        Talking = false;
        isEnd = true;
    }

    void Chosen()
    {
        chosenSTC = "";

        while (givenNum[noteCount])
        {
            noteCount++;
            if(noteCount > 5)
                noteCount = 0;
        }



        switch (noteCount)
        {
            case 0:
                chosenSTC = chosenSentence_0[preCount];
                aniNum = 0;
                break;
            case 1:
                chosenSTC = chosenSentence_1[preCount];
                aniNum = 0.16f; 
                break;
            case 2:
                chosenSTC = chosenSentence_2[preCount];
                aniNum = 0.33f; 
                break;
            case 3:
                chosenSTC = chosenSentence_3[preCount];
                aniNum = 0.5f; 
                break;
            case 4:
                chosenSTC = chosenSentence_4[preCount];
                aniNum = 0.66f;
                break;
            case 5:
                chosenSTC = chosenSentence_5[preCount];
                aniNum = 0.83f;
                break;
        }
        text.text = "";

        for (int i = 0; i < chosenSTC.Length; i++)
        {
            text.text += chosenSTC[i];
        }
        
        // 선택되지 않은 패널의 색을 연하게 함
        aniNotes.SetFloat("ChosenNum", aniNum);
    }

    public bool[] GetIsChoice(int length)
    {
        bool[] _BoolArray = new bool[length];
        for (int i = 0; i < length; i++)
        {
            _BoolArray[i] = note[i];
        }
        return _BoolArray;
    }
}
