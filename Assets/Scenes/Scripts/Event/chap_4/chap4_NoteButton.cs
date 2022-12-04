using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chap4_NoteButton : MonoBehaviour
{
    [SerializeField] DialogueBox[] dialogueBox = new DialogueBox[9];
    public bool[] note = new bool[9];

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void InputButton(int _count)
    {
        if (note[_count] && !dialogueBox[_count].isLog)
        {
            dialogueBox[_count].SetDialogue();
        }
    }

    public void ClickButton(int _count)
    {
        InputButton(_count);
    }

    public void ChangeLog(int _num, Dialogue[] _logs)
    {
        if(_logs.Length != 0)
        {
            dialogueBox[_num].dialogues = new Dialogue[_logs.Length];
            dialogueBox[_num].dialogues = _logs;
            dialogueBox[_num].isRepeat = true;
            note[_num] = true;
        }
    }
}
