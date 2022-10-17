using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBed : MonoBehaviour
{
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform OneBg;
    [SerializeField] private TestChat testChat;
    [SerializeField] private GameObject bed;
    [SerializeField] private GameObject idle_bed;
    [SerializeField] private GameObject npc;
    [SerializeField] private GameObject npc2;
    private bool isStart;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!isStart && testChat.isTarget && other.tag == "Player")
        {
            Invoke("Fade", 2f);
            isStart = true;
        }
    }

    private void Fade()
    {
        player.transform.position = OneBg.position;
        fade.SetActive(true);

        idle_bed.GetComponent<BoxCollider2D>().isTrigger = true;
        idle_bed.GetComponent<Bed>().isBed = true;
        npc.GetComponent<TestChat>().isTarget = true;
        npc.GetComponent<NPC>().Active();
        npc2.SetActive(false);
    }
}
