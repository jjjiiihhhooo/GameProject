using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_H2_Event : MonoBehaviour
{
    GameObject player;
    CameraManager cameraManager;

    public string startPoint;
    [SerializeField] GameObject jY;
    DialogueBox dialogueBox;
    [SerializeField] GameObject hairPin;
    [SerializeField] GameObject end;
    [SerializeField] GameObject room;

    void Start()
    {
        player = GameObject.Find("Player");
        
        cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.isCamera = false;
        cameraManager.Transform(room.transform);

        player.GetComponent<Animator>().SetFloat("DirX", 1);
        player.GetComponent<Animator>().SetFloat("DirY", 0);
        player.GetComponent<PlayerMove>().isMove = true;
        player.GetComponent<PlayerMove>().inEvent = true;
        player.transform.position = this.gameObject.transform.position;
        dialogueBox = jY.GetComponent<DialogueBox>();

        StartCoroutine(EndEvent());
    }

    IEnumerator EndEvent()
    {
        // 초기 대기 시간
        yield return new WaitForSeconds(5.0f);

        // 지연과의 대화 시작
        dialogueBox.SetDialogue();

        // 대화가 지속되는 동안
        while(!dialogueBox.noMore)
        {
            yield return null;
        }

        // 지연 오브젝트 비활성화, 머리핀 활성화
        jY.SetActive(false);
        hairPin.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        end.SetActive(true);
    }
}
