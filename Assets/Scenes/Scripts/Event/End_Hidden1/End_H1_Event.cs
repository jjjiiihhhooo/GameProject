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
    Animator ani;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        ani = GetComponent<Animator>();

        ani.SetFloat("DirX", 1);
        ani.SetFloat("DirY", 0);

        StartCoroutine(Event());
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(3.0f);
        dialogueBox.SetDialogue();

        while(!dialogueBox.noMore)
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
