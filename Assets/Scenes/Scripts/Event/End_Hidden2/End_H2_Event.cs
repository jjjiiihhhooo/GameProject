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
        player = GameObject.FindWithTag("Player");
        
        cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.isCamera = false;
        cameraManager.Transform(room.transform);

        player.GetComponent<PlayerMove>().isMove = true;
        player.GetComponent<PlayerMove>().inEvent = true;
        player.transform.position = this.gameObject.transform.position;
        dialogueBox = jY.GetComponent<DialogueBox>();

        StartCoroutine(EndEvent());
    }

    
    void Update()
    {
        
    }

    IEnumerator EndEvent()
    {
        // �ʱ� ��� �ð�
        yield return new WaitForSeconds(5.0f);

        // �������� ��ȭ ����
        dialogueBox.SetDialogue();

        // ��ȭ�� ���ӵǴ� ����
        while(!dialogueBox.noMore)
        {
            yield return null;
        }

        // ���� ������Ʈ ��Ȱ��ȭ, �Ӹ��� Ȱ��ȭ
        jY.SetActive(false);
        hairPin.SetActive(true);

        yield return new WaitForSeconds(5.0f);

        end.SetActive(true);
    }
}
