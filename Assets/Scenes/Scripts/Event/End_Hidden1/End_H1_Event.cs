using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class End_H1_Event : MonoBehaviour
{
    DialogueBox dialogueBox;
    [SerializeField] GameObject player;
    [SerializeField] GameObject hanaIllust;
    [SerializeField] GameObject fade;
    DialogueManager DM;
    Animator ani;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        DM = FindObjectOfType<DialogueManager>();
        ani = GetComponent<Animator>();

        ani.SetFloat("DirX", 1);
        ani.SetFloat("DirY", 0);

        StartCoroutine(Event());
    }

    void Update()
    {
        DM.GetKey = false;
    }

        IEnumerator Event()
    {
        yield return new WaitForSeconds(3.0f);
        dialogueBox.SetDialogue();
        yield return new WaitForSeconds(2.0f);
        DM.StartLog();
        yield return new WaitForSeconds(2.0f);
        DM.StartLog();
        yield return new WaitForSeconds(2.0f);
        DM.StartLog();
        yield return new WaitForSeconds(2.0f);
        DM.StartLog();
        yield return new WaitForSeconds(2.0f);
        DM.ExitDialogue();

        while (!dialogueBox.noMore)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        fade.GetComponent<FadeInOut>().StartFade("out","black",1.5f);

        yield return new WaitForSeconds(0.5f);

        while (!fade.GetComponent<FadeInOut>().isOn)
        {
            yield return null;
        }

        hanaIllust.SetActive(true);
        fade.GetComponent<FadeInOut>().StartFade("in", "black", 1.5f);
    }
}
