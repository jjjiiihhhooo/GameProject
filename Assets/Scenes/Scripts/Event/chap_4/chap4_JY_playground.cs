using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_JY_playground : MonoBehaviour
{
    public int count = 0;
    [SerializeField] GameObject ranbox;
    [SerializeField] GameObject giveNote;

    [SerializeField] GameObject player;
    SceneTransfer sceneTransfer;
    SceneChanger sceneChanger;
    SpriteRenderer spr;

    chap4_NoteBox NB;

    void Start()
    {
        NB = giveNote.GetComponent<chap4_NoteBox>();
        sceneTransfer = GetComponent<SceneTransfer>();
        sceneChanger = FindObjectOfType<SceneChanger>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 쪽지를 다 모았을 때
        if(count == 6)
        {
            count = 0;
            ranbox.SetActive(false);
            giveNote.SetActive(true);
        }

        // 올바른 쪽지를 주었을 때
        if (NB.noMore)
        {
            StartCoroutine(GoEnd());
        }
    }

    IEnumerator GoEnd()
    {
        player.GetComponent<PlayerMove>().inEvent = true;
        player.GetComponent<Animator>().SetFloat("DirY", 0.0f);
        player.GetComponent<Animator>().SetFloat("DirX", 1.0f);

        yield return new WaitForSeconds(1.0f);

        // chap4에서 정답을 맞추지 못했을 경우
        if(!sceneChanger.chap4JY)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0);
        }

        yield return new WaitForSeconds(2.0f);

        player.GetComponent<PlayerMove>().inEvent = false;
        // 히든
        if (sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden1");
        }
        // 노말
        else if (sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Bad");
        }
        // 히든2
        else if(!sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
        // 배드
        else if(!sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Bad");
        }
    }
}
