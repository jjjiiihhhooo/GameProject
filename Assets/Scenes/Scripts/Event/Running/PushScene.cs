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
    [SerializeField] AudioSource audioSource;
    public float moveSpeed;
    public float speedDecrease;
    private float currentSpeed;

    void Start()
    {
        audioSource = GameObject.FindWithTag("Canvas").transform.Find("SoundManager").gameObject.GetComponent<AudioSource>();
        chaseScene = FindObjectOfType<ChaseScene>();
        //if(player == null)
        //    player = GameObject.FindWithTag("Player");
        //if(mainCamera == null)
        //    mainCamera = GameObject.FindWithTag("MainCamera");
        animator = player.GetComponent<Animator>();

        currentSpeed = moveSpeed;
    }

    public void EndOfAlley()
    {
        if (speedDecrease <= 0)
            speedDecrease = 0.1f;
        StartCoroutine(StopRun()); // 플레이어가 화면 전환 이후에 달리기를 멈춤
    }

    private IEnumerator StopRun()
    {
        mainCamera.GetComponent<CameraManager>().isChase = false;
        mainCamera.GetComponent<CameraManager>().Bool();
        player.transform.position = mapTransform.position;

        while (currentSpeed > 0)
        {
            player.transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(Time.deltaTime);
            currentSpeed -= speedDecrease;
            if (audioSource.volume > 0)
                audioSource.volume -= 0.01f;
            if (currentSpeed < moveSpeed / 4)
            {
                audioSource.volume = 0;
                break;
            }
        }
        animator.SetBool("Walk", false);
        player.GetComponent<PlayerMove>().inEvent = false;
    }
}
