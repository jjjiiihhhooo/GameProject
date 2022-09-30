using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    
    public static ChoiceManager instance;
    private ChoiceEvent theChoiceEvent;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    } //싱글톤

    private string question;
    private List<string> answerList;

    public GameObject go;
    public GameObject npcGameObject;

    public Text question_Text;
    public Text[] answer_Text;
    public GameObject[] answer_Panel;

    public Animator animator;

    public string KeySound;
    public string enterSound;

    public bool choiceIng; //선택지가 이루어지고있는지 ing
    public bool isChoice;
    public bool isChoice2;
    private bool keyInput;

    private int count; //배열 크기

    private int result; //선택창 확인

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Start()
    {
        animator = GetComponent<Animator>();
        theChoiceEvent = FindObjectOfType<ChoiceEvent>();
        answerList = new List<string>();
        for(int i = 0; i<= 3; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        question_Text.text = "";

    }

    public void NpcChange(GameObject _gameObject)
    {
        npcGameObject = _gameObject;
    }


    public void ShowChoice(Choice _choice)
    {
        go.SetActive(true);
        result = 0;

        question = _choice.question;
        for(int i = 0; i< _choice.answers.Length; i++)
        {
            answerList.Add(_choice.answers[i]);
            answer_Panel[i].SetActive(true);
            count = i;
        }
        animator.SetBool("Appear", true);
        Selection();
        StartCoroutine(ChoiceCoroutine());
    }

    private IEnumerator ChoiceCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        StartCoroutine(TypingQuestion());
        StartCoroutine(TypingAnswer_0());
        if (count >= 1)
            StartCoroutine(TypingAnswer_1());
        if (count >= 2)
            StartCoroutine(TypingAnswer_2());
        if (count >= 3)
            StartCoroutine(TypingAnswer_3());

        yield return new WaitForSeconds(0.5f);

        keyInput = true;
        
    }

    private IEnumerator TypingQuestion()
    {
        for(int i = 0; i < question.Length; i++)
        {
            question_Text.text += question[i];
            yield return waitTime;
        }
    }

    private IEnumerator TypingAnswer_0()
    {
        yield return new WaitForSeconds(0.4f);
        for (int i = 0; i < answerList[0].Length; i++)
        {
            answer_Text[0].text += answerList[0][i];
            yield return waitTime;
        }
    }

    private IEnumerator TypingAnswer_1()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < answerList[1].Length; i++)
        {
            answer_Text[1].text += answerList[1][i];
            yield return waitTime;
        }
    }

    private IEnumerator TypingAnswer_2()
    {
        yield return new WaitForSeconds(0.6f);
        for (int i = 0; i < answerList[2].Length; i++)
        {
            answer_Text[2].text += answerList[2][i];
            yield return waitTime;
        }
    }

    private IEnumerator TypingAnswer_3()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < answerList[3].Length; i++)
        {
            answer_Text[3].text += answerList[3][i];
            yield return waitTime;
        }
    }

    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInput = false;
                if(result == count)
                {
                    ExitChoice();
                }
                else
                {
                    theChoiceEvent.Action(result);
                    ExitChoice();
                }
                
            }
        }
        else if (isChoice == true && Input.GetKeyDown(KeyCode.Space))
        {
            npcGameObject.GetComponent<ChoiceEvent>().StartCoroutine();
            isChoice2 = true;
        }

    }

    public void Selection()
    {
        Color color = answer_Panel[0].GetComponent<Image>().color;
        color.a = 0.75f;
        for(int i = 0; i <= count; i++)
        {
            answer_Panel[i].GetComponent<Image>().color = color;
        }
        color.a = 1f;
        answer_Panel[result].GetComponent<Image>().color = color;
    }

    public int GetResult()
    {
        return result;
    }

    public void ExitChoice()
    {
        question_Text.text = "";
        for (int i = 0; i <= count; i++)
        {
            answer_Text[i].text = "";
            answer_Panel[i].SetActive(false);
        }
        answerList.Clear();
        animator.SetBool("Appear", false);
        choiceIng = false;
        go.SetActive(false);
        isChoice2 = false;
    }
}
