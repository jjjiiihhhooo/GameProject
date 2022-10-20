using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject black; // 초기 화면 왼쪽의 검은 부분. 플레이어가 화면 왼쪽에서부터 움직이는 착시
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"러닝"의 이미지를 가진 오브젝트 배열
    [SerializeField] Transform scenePosition; // 해당 씬 카메라 위치의 좌표
    Animator chaseAnimator;
    int length;
    
    public bool isChase = false;

    float width = 12.8f; // "러닝"의 x폭

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // 배경이 지나치는 속도. 상대적 플레이어 x축 이동 속도
    public float ySpeed = 10; // 플레이어의 y축 이동 속도

    void Start()
    {
        length = backGround.Length;
        chaseAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (isChase)
        {
            StartCoroutine(MoveChase());
        }
        else if (!isChase)
        {
            StopCoroutine(MoveChase());
        }
    }

    private IEnumerator MoveChase()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // horizontal값에 의해서 애니메이션만 출력
        if(horizontal != 0)
            chaseAnimator.SetFloat("DirX", horizontal);
        chaseAnimator.SetBool("Walk", horizontal == 0? false : true);

        // 입력한 vertical값에 따라 플레이어의 y값만 이동
        player.transform.Translate(0, vertical * xSpeed * Time.deltaTime, 0);

        //입력한 horizontal값에 따라 검은 배경의 x값만 이동
        if (black.transform.position.x >= scenePosition.position.x - width - 1)
            black.transform.Translate(- horizontal * ySpeed * Time.deltaTime, 0, 0);

        for (int i = 0; i < length; i++)
        {
            backGround[i].transform.Translate(- horizontal * ySpeed * Time.deltaTime, 0, 0);

            if (backGround[i].transform.position.x <= scenePosition.position.x - width - 1) // backGround[i]가 카메라에서 보이지 않을 정도로 밀려났을 때
            {
                backGround[i].transform.Translate(scenePosition.position.x + width - 1.1f, 0, 0); // 해당 오브젝트를 화면 오른쪽에 옮김
            }
        }
        yield return null;
    }
}
