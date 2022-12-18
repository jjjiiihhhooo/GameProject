using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SaveCheck : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject saveChat;
    [SerializeField] private GameObject blackDisplay;
    [SerializeField] private string text;
    [SerializeField] private int checkCount;
    [SerializeField] private int saveSceneCount;
    [SerializeField] private string SceneName;
    //private TestChat testChat;

    [SerializeField] FadeInOut fade;
    [SerializeField] GameObject player;

    DialogueBox dialogueBox;
    SoundManager soundManager;
    private bool isStart;

    private void Awake()
    {
        //testChat = GetComponent<TestChat>();
        dialogueBox = GetComponent<DialogueBox>();
        soundManager= GetComponent<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.tag == "Player")
        {
            

            if (!isStart && checkCount == 1)
            {
                isStart = true;
                saveManager.saveData.sceneName = SceneName;
                saveManager.IsSave();
                saveChat.GetComponent<Text>().text = text;
                Invoke("SaveChatTrue", 2f);

                if (blackDisplay != null)
                {
                    blackDisplay.SetActive(true);
                    StartCoroutine(Chat());
                    Invoke("DisplayFalse", 4f);
                }

            }
            else if(!isStart)
            {
                isStart = true;
                saveChat.GetComponent<Text>().text = text;
                Invoke("SaveChatTrue", 2f);
            }
        }
    }

    IEnumerator Chat()
    {
        //testChat.isTarget = false;
        //testChat.Chat();
        soundManager.PlaySound(0, false, false, 1);
        yield return new WaitForSeconds(1.3f);
        soundManager.PlaySound(1, false, false, 1);
        fade.StartFade("in", "black", 1.0f);
        yield return new WaitForSeconds(1.3f);
        dialogueBox.isTrigger= false;
        player.GetComponent<PlayerMove>().inEvent = false;
        dialogueBox.SetDialogue();
    }

    private void DisplayFalse()
    {
        //fade.SetActive(true);
        blackDisplay.SetActive(false);
    }

    private void SaveChatFalse()
    {
        if (saveChat != null)
            saveChat.SetActive(false);
    }

    private void SaveChatTrue()
    {
        if(saveChat != null)
            saveChat.SetActive(true);
        Invoke("SaveChatFalse", 2f);
    }
}
