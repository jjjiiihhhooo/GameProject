using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Act2 : MonoBehaviour
{
    [SerializeField] FadeInOut fade;
    [SerializeField] Transform mapTransform2;
    [SerializeField] GameObject player;
    [SerializeField] CameraManager main_camera;
    [SerializeField] QuestManager theQuestManager;

    public void StartAct2()
    {
        StartCoroutine(CAct2());
    }

    IEnumerator CAct2()
    {
        player.GetComponent<PlayerMove>().inEvent = true;
        fade.StartFade("out", "black", 1.0f);
        while (!fade.isOn) yield return null;
        player.transform.position = mapTransform2.position;
        main_camera.Bool();
        theQuestManager.questcount = -1;
        theQuestManager.chap2_questText();
    }
}
