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
        // ������ �� ����� ��
        if(count == 6)
        {
            count = 0;
            ranbox.SetActive(false);
            giveNote.SetActive(true);
        }

        // �ùٸ� ������ �־��� ��
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

        // chap4���� ������ ������ ������ ���
        if(!sceneChanger.chap4JY)
        {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0);
        }

        yield return new WaitForSeconds(2.0f);

        player.GetComponent<PlayerMove>().inEvent = false;
        // ����
        if (sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden1");
        }
        // �븻
        else if (sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Bad");
        }
        // ����2
        else if(!sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
        // ���
        else if(!sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Bad");
        }
    }
}
