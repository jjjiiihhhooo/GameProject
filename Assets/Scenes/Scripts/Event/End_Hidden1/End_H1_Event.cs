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

    AudioSource audioSource;
    SceneTransfer sceneTransfer;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        ani = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        sceneTransfer = GetComponent<SceneTransfer>();

        ani.SetFloat("DirX", 1);
        ani.SetFloat("DirY", 0);

        StartCoroutine(Event());
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(3.0f);
        dialogueBox.SetDialogue();

        while (!dialogueBox.noMore)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        fade.GetComponent<FadeInOut>().StartFade("out","black",1.5f);

        yield return new WaitForSeconds(0.5f);
        audioSource.Play();

        while (!fade.GetComponent<FadeInOut>().isOn)
        {
            yield return null;
        }

        hanaIllust.SetActive(true);
        fade.GetComponent<FadeInOut>().StartFade("in", "black", 1.5f);

        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneTransfer.TransScene("Title");
                break;
            }
            yield return null;
        }
    }
}
