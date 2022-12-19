using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScene : MonoBehaviour
{
    [SerializeField] ChaseScene chaseScene;
    [SerializeField] Transform mapTransform;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;
    Animator animator;
    GameObject SM;
    AudioSource audioSource;
    public float moveSpeed;
    public float speedDecrease;
    private float currentSpeed;

    void Start()
    {
        SM = GameObject.FindWithTag("sound");
        audioSource = SM.GetComponent<AudioSource>();
        chaseScene = FindObjectOfType<ChaseScene>();

        //player = GameObject.Find("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");

        //if(player == null)
        //    player = GameObject.FindWithTag("Player");
        //if(mainCamera == null)
        //    mainCamera = GameObject.FindWithTag("MainCamera");

        animator = player.GetComponent<Animator>();
        animator.SetBool("Walk", true);
        player.GetComponent<PlayerMove>().indep = false;
        currentSpeed = moveSpeed;
    }

    public void EndOfAlley()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
        StartCoroutine(StopRun()); // �÷��̾ ȭ�� ��ȯ ���Ŀ� �޸��⸦ ����
    }

    private IEnumerator StopRun()
    {
        mainCamera.GetComponent<CameraManager>().isChase = false;
        mainCamera.GetComponent<CameraManager>().Bool();
        player.transform.position = mapTransform.position;

        while (currentSpeed > 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.right * currentSpeed;
            //player.transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(Time.deltaTime);
            currentSpeed -= speedDecrease;
            if (audioSource.volume > 0)
                audioSource.volume -= 0.01f;
            if (currentSpeed < moveSpeed / 4)
            {
                audioSource.volume = 0;
                SM.GetComponent<MusicPlayer>().DontDestroy = false;
                Destroy(SM);
                break;
            }
        }
        animator.SetBool("Walk", false);
        player.GetComponent<PlayerMove>().inEvent = false;
        player.GetComponent<PlayerMove>().indep = true;
    }
}
