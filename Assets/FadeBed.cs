using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBed : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject player;
    //[SerializeField] private Transform OneBg;
    //[SerializeField] private TestChat testChat;
    //[SerializeField] private GameObject bed;
    [SerializeField] private GameObject idle_bed;
    [SerializeField] private GameObject npc;
    [SerializeField] Blink blink;
    //[SerializeField] private GameObject npc2;
    DialogueBox dialogueBox;
    int phase = 0;

    private void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        StartCoroutine(StartFade());
    }
    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    Debug.Log($"{dialogueBox.noMore} && {other.tag}");
    //    if(!isStart && dialogueBox.noMore && other.tag == "Player")
    //    {
    //        Fade();
    //        isStart = true;
    //    }
    //}

    private void Update()
    {
        //if (phase == 0 && dialogueBox.noMore)
        //{
        //    Fade();
        //    phase++;
        //}
        //if(phase == 1 && npc.activeSelf)
        //{
        //    npc.GetComponent<DialogueBox>().isTrigger = true;
        //    npc.GetComponent<NPC>().Active();
        //    gameObject.SetActive(false);
        //    phase++;
        //}
    }

    void Fade()
    {
        //player.transform.position = OneBg.position;
        fade.SetActive(true);

        idle_bed.GetComponent<BoxCollider2D>().isTrigger = true;
        idle_bed.GetComponent<Bed>().isBed = true;
        npc.SetActive(true);
    }

    IEnumerator StartFade()
    {
        while(!dialogueBox.noMore) yield return null;

        idle_bed.GetComponent<BoxCollider2D>().isTrigger = true;
        idle_bed.GetComponent<Bed>().isBed = true;
        npc.SetActive(true);

        // ±ôºý ±ôºý
        blink.StartBlink("close", 0.15f);
        yield return new WaitForSeconds(0.14f);
        while (blink.isOpen) yield return null;

        blink.StartBlink("open", 0.15f);
        yield return new WaitForSeconds(0.14f);
        while (!blink.isOpen) yield return null;

        yield return new WaitForSeconds(0.3f);

        blink.StartBlink("close", 0.15f);
        yield return new WaitForSeconds(0.14f);
        while (blink.isOpen) yield return null;

        npc.GetComponent<DialogueBox>().isTrigger = true;
        npc.GetComponent<NPC>().Active();
        gameObject.SetActive(false);

        blink.StartBlink("open", 0.15f);
        yield return new WaitForSeconds(0.14f);
        while (!blink.isOpen) yield return null;

        //blink.StartBlink("close", 0.15f);
        //yield return new WaitForSeconds(0.1f);
        //while (blink.isOpen) yield return null;

        //blink.StartBlink("open", 0.15f);
        //yield return new WaitForSeconds(0.1f);
        //while (!blink.isOpen) yield return null;

        //yield return new WaitForSeconds(0.3f);

        //blink.StartBlink("close", 0.15f);
        //yield return new WaitForSeconds(0.1f);
        //while (blink.isOpen) yield return null;

        //blink.StartBlink("open", 0.15f);
        //yield return new WaitForSeconds(0.1f);
        //while (!blink.isOpen) yield return null;
    }
}
