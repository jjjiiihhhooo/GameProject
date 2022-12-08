using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushScene : MonoBehaviour
{
    [SerializeField] ChaseScene chaseScene;
    [SerializeField] Transform mapTransform;
    private GameObject player;
    private GameObject mainCamera;
    Animator animator;
    AudioSource audioSource;
    public float moveSpeed;
    public float speedDecrease;
    private float currentSpeed;

    void Start()
    {
        audioSource = GameObject.FindWithTag("Canvas").transform.Find("SoundManager").gameObject.GetComponent<AudioSource>();
        chaseScene = FindObjectOfType<ChaseScene>();
        player = GameObject.Find("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
        animator = player.GetComponent<Animator>();

        currentSpeed = moveSpeed;
    }

    public void EndOfAlley()
    {
        player.GetComponent<Rigidbody2D>().velocity = Vector2.right * moveSpeed;
        StartCoroutine(StopRun()); // 플레이어가 화면 전환 이후에 달리기를 멈춤
    }

    private IEnumerator StopRun()
    {
        mainCamera.GetComponent<CameraManager>().isChase = false;
        player.transform.position = mapTransform.position;

        while (currentSpeed > 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.right * currentSpeed;
            //player.transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            yield return null;
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
