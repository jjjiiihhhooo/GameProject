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
    [SerializeField] Blink blink;
    AudioSource audioSource;
    SceneTransfer sceneTransfer;

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
        
        audioSource = GetComponent<AudioSource>();
        sceneTransfer = GetComponent<SceneTransfer>();

        StartCoroutine(EndEvent());
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
        yield return new WaitForSeconds(1.0f);

        audioSource.Play();

        yield return new WaitForSeconds(2.0f);
        blink.StartBlink("close", 3f);
        yield return new WaitForSeconds(0.3f);
        while(blink.isOpen) yield return null;

        end.SetActive(true);

        yield return new WaitForSeconds(5.0f);
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                sceneTransfer.TransScene("Title");
                break;
            }
            yield return null;
        }
    }
}
