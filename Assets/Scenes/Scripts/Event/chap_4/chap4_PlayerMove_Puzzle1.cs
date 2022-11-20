using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class chap4_PlayerMove_Puzzle1 : MonoBehaviour
{
    [SerializeField] chap4_BlackMob blackMob;
    [SerializeField] Transform O_point; // 좌표 좌측 상단
    [SerializeField] Transform P_point; // 좌표 우측 하단
    [SerializeField] GameObject alleyScene;
    [SerializeField] GameObject topWall;
    [SerializeField] GameObject vanguard;

    // 맵의 가로x세로 값
    int mapX;
    int mapY;
    // 맵의 가로 세로 폭
    float width;
    float height;
    // 플레이어 위치
    int posX;
    int posY;

    chap4_MapSpawner mapSpawner;
    GameObject player;
    Animator p_ani;

    // 벡터 입력값
    float speed = 6.0f;
    float dirX;
    float dirY;

    // 가장 최근에 받은 입력값 계산
    int timerX;
    int timerXL;
    int timerXR;
    int timerY;
    int timerYU;
    int timerYD;

    public bool canMove = true;
    bool getKey = true;
    bool singleCall_0 = true;
    //bool singleCall_1 = true;
    int c = 0;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        p_ani = player.GetComponent<Animator>();
        mapSpawner = GetComponent<chap4_MapSpawner>();

        mapX = mapSpawner.x; 
        mapY = mapSpawner.y;
        posX = mapSpawner.x - 1;
        posY = mapSpawner.y - 1;

        vanguard.transform.position = P_point.position + new Vector3(0, 0.925f, 0);

        width = (Mathf.Abs(O_point.position.x - P_point.position.x) / (mapX - 1));
        height = (Mathf.Abs(O_point.position.y - P_point.position.y) / (mapY - 1));
    }

    void Update()
    {
        if (mapSpawner.GetGetReady() && canMove && singleCall_0)
        {
            topWall.SetActive(false);
            StartCoroutine(BoardMove());
        }
    }

    IEnumerator BoardMove()
    {
        c++;
        singleCall_0 = false;

        // 입력값에 따른 dirX, dirY 초기화
        dirX = Input.GetAxisRaw("Horizontal") == 1 ? 1 : (Input.GetAxisRaw("Horizontal") == -1 ? -1 : 0 );
        dirY = Input.GetAxisRaw("Vertical") == 1 ? 1 : (Input.GetAxisRaw("Vertical") == -1 ? -1 : 0);

        // 누르는 동안 타이머 증가
        timerXR = dirX == 1 ? ++timerXR : 0;
        timerXL = dirX == -1 ? ++timerXL : 0;
        timerYU = dirY == 1 ? ++timerYU : 0;
        timerYD = dirY == -1 ? ++timerYD : 0;

        // 키입력이 있을 때
        if ((dirX != 0 || dirY != 0) && getKey)
        {
            // 최근에 누른 타이머 선별 (타이머가 적게 흘렀으며, 0이 아닌 경우)
            timerX = timerXR < timerXL ? (timerXR != 0 ? timerXR : timerXL) : (timerXR == 0 ? timerXR : timerXL);
            timerY = timerYU < timerYD ? (timerYU != 0 ? timerYU : timerYD) : (timerYD == 0 ? timerYU : timerYD);
            // 최근에 누른 방향 추출
            if ((timerX < timerY && timerX != 0) || timerY == 0)
                dirY = 0;
            else if (timerY < timerX && timerY != 0 || timerX == 0)
                dirX = 0;

            // 이동 방향 정수형으로 변환
            int iX = dirX == 1 ? 1 : (dirX == -1 ? -1 : 0);
            int iY = dirY == 1 ? -1 : (dirY == -1 ? 1 : 0);
            //Debug.Log($"{posX + iX < 0 || posX + iX >= mapX}");
            //Debug.Log($"{posY + iY < 0 || posY + iY >= mapY}");
            //Debug.Log($"{mapSpawner.Map[posY + iY, posX + iX] == 1}");
            //Debug.Log($"{c}. pos: {posX}, {posY} // dir: {dirX}, {dirY} // i: {iX}, {iY}");
            //Debug.Log($"{c}. posTo: {posX + iX}, {posY + iY}");
            //Debug.Log($"{c}. if ({!(posX + iX < 0 || posX + iX >= mapX || posY + iY < 0 || posY + iY >= mapY || mapSpawner.Map[posY + iY, posX + iX] == 1)})");

            // 맵 범위를 벗어나거나, 벽이 아닐 경우 or 목적지인 경우
            if (!(posX + iX < 0 || posX + iX >= mapX 
                || posY + iY < 0 || posY + iY >= mapY 
                || mapSpawner.Map[posY + iY, posX + iX] == 1)
                || (posX + iX == 8 && posY + iY == -1))
            {
                // 플레이어 위치 초기화
                // 목적지 도달시 각각 X = 8, Y = -2
                posX += 2 * iX;
                posY += 2 * iY;

                // 목적지 위치 초기화
                vanguard.transform.Translate(new Vector3(2 * dirX * width, 2 * dirY * height, 0));
                yield return null;

                // 애니메이션 출력
                p_ani.SetFloat("DirX", dirX);
                p_ani.SetFloat("DirY", dirY);
                p_ani.SetBool("Walk", true);

                // 플레이어 이동
                while (true)
                {
                    if (getKey)
                    {
                        getKey = false;
                    }
                    player.transform.position = Vector2.MoveTowards(player.transform.position, vanguard.transform.position, speed * Time.deltaTime);
                    yield return null;
                    if (Vector2.Distance(vanguard.transform.position, player.transform.position) < 0.001f)
                    {
                        p_ani.SetBool("Walk", false);
                        blackMob.SetPos(posX, posY);
                        break;
                    }
                }
                // 목적지인 경우 방 탈출. 퍼즐 종료
                if (posX == 8 && posY == -2)
                {
                    player.GetComponent<PlayerMove>().inEvent = false;
                    yield return new WaitForSeconds(0.3f);
                    mapSpawner.SetGetReady(false);
                    alleyScene.SetActive(true);
                }
            }
            getKey = true;
        }
        singleCall_0 = true;
    }

    public int PosX
    {
        get { return posX; }
    }
    public int PosY
    {
        get { return posY; }
    }
}
