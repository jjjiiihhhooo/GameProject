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
    public float moveSpeed;
    public float speedDecrease;
    private float currentSpeed;

    void Start()
    {
        chaseScene = FindObjectOfType<ChaseScene>();
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
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
        player.transform.position = mapTransform.position;
        
        while (currentSpeed > 0)
        {
            player.transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
            yield return new WaitForSeconds(Time.deltaTime);
            currentSpeed -= speedDecrease;
            if (currentSpeed < moveSpeed / 4)
                break;
        }
        animator.SetBool("Walk", false);
        player.GetComponent<PlayerMove>().inEvent = false;
    }
}
