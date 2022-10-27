using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject BlackMob;
    [SerializeField] GameObject black; // 초기 화면 왼쪽의 검은 부분. 플레이어가 화면 왼쪽에서부터 움직이는 착시
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"러닝"의 이미지를 가진 오브젝트 배열
    [SerializeField] Transform mapTransform; // 맵 위치 좌표
    Animator chaseAnimator;
    int length;

    public bool isChase = false;

    float width = 12.8f; // "러닝"의 x폭

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // 배경이 지나치는 속도. 상대적 플레이어 x축 이동 속도
    public float ySpeed = 10; // 플레이어의 y축 이동 속도

    public Vector3 mobLocation;
    public Vector3 cameraLocation;

    void Start()
    {
        mobLocation = mapTransform.position + new Vector3(-9, -0.5f, 0);
        cameraLocation = mapTransform.position + new Vector3(6, 2, -1);
        length = backGround.Length;
        chaseAnimator = player.GetComponent<Animator>();
    }

    void Update()
    {
        if (isChase) // isChase값에 따라 MoveChase() 실행 또는 중단
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
        /*
         * 플레이어의 경우에는 애니메이션을 출력하면서 위치는 y축 이동만 한다. 
         * 배경은 입력한 horizontal값에 따라 x축으로 컨베이어 벨트 방식처럼 이동한다.
         * 이에 따라 상대적으로 플레이어가 배경에 비해 x축으로 움직이는 것 같이 보인다.
         */

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // 플레이어, 입력한 vertical값에 따라 y값만 이동
        player.transform.Translate(0, vertical * xSpeed * Time.deltaTime, 0);

        // 카메라, 입력한 horizontal값에 따라 x값만 이동. 화면상 오른쪽 1/3 지점에 플레이어 위치
        if (mainCamera.transform.position.x >= mapTransform.position.x - 4)
            mainCamera.transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

        // 애니메이션, horizontal값에 의해서 출력
        if (horizontal != 0)
            chaseAnimator.SetFloat("DirX", horizontal);
        chaseAnimator.SetBool("Walk", horizontal == 0 ? false : true);

        // 검은형체, 입력한 horizontal값에 따라 x값만 이동
        BlackMob.transform.Translate((1 - horizontal) * xSpeed * Time.deltaTime, 0, 0);

        // 검은 배경, 입력한 horizontal값에 따라 x값만 이동
        if (black.transform.position.x >= mapTransform.position.x - 1.8f * width)
            black.transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);
        for (int i = 0; i < length; i++)
        {
            backGround[i].transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

            if (backGround[i].transform.position.x <= mapTransform.position.x - (2.2f * width) + 0.1f) // backGround[i]가 카메라에서 보이지 않을 정도로 밀려났을 때
            {
                float newX = mapTransform.position.x + (1.3f * width) - 0.2f;

                backGround[i].transform.position = new Vector3(newX, this.transform.position.y, 0); // 해당 오브젝트를 화면 오른쪽에 옮김
            }
        }
        yield return null;
    }
}
