using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_JY_playground : MonoBehaviour
{
    public int count = 0;
    [SerializeField] GameObject ranbox;
    [SerializeField] GameObject giveNote;

    GameObject player;
    SceneTransfer sceneTransfer;
    SceneChanger sceneChanger;

    chap4_NoteBox NB;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        NB = giveNote.GetComponent<chap4_NoteBox>();
        sceneTransfer = GetComponent<SceneTransfer>();
        sceneChanger = FindObjectOfType<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
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
            this.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(2.0f);

        player.GetComponent<PlayerMove>().inEvent = false;
        // 히든
        if (sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
        // 노말
        else if (sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
        // 히든2
        else if(!sceneChanger.chap3JY && sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
        // 배드
        else if(!sceneChanger.chap3JY && !sceneChanger.chap4JY)
        {
            sceneTransfer.TransScene("Ending_Hidden2");
        }
    }
}
