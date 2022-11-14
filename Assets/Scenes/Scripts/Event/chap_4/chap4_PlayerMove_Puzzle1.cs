using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class chap4_PlayerMove_Puzzle1 : MonoBehaviour
{
    [SerializeField] Transform O_point; // ��ǥ ���� ���
    [SerializeField] Transform P_point; // ��ǥ ���� �ϴ�
    [SerializeField] GameObject sp;

    // ���� ����x���� ��
    int mapX;
    int mapY;
    // ���� ���� ���� ��
    float width;
    float height;
    // �÷��̾� ��ġ
    int posX;
    int posY;

    chap4_MapSpawner chap4_mapSpawner;
    chap4_MapSpawner MS;
    GameObject player;
    Rigidbody2D p_rigid;
    Animator p_ani;

    // ���� �Է°�
    float speed = 8.0f;
    float x;
    float y;

    // ���� �ֱٿ� ���� �Է°� ���
    float timerX;
    float timerXL;
    float timerXR;
    float timerY;
    float timerYU;
    float timerYD;

    bool getKey = true;
    bool singleCall_0 = true;
    bool singleCall_1 = true;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        p_rigid = player.GetComponent<Rigidbody2D>();
        p_ani = player.GetComponent<Animator>();
        chap4_mapSpawner = GetComponent<chap4_MapSpawner>();
        MS = GetComponent<chap4_MapSpawner>();

        mapX = MS.x; 
        mapY = MS.y;
        posX = MS.x;
        posY = MS.y;

        width = (Mathf.Abs(O_point.position.x - P_point.position.x) / (mapX - 1));
        height = (Mathf.Abs(O_point.position.y - P_point.position.y) / (mapY - 1));

        sp.transform.position = P_point.position;
    }

    void Update()
    {
        mapX = MS.x;
        mapY = MS.y;
        Debug.Log($" if {chap4_mapSpawner.GetGetReady()},{singleCall_0}");
        if (chap4_mapSpawner.GetGetReady() && singleCall_0)
        {
            Debug.Log("Co");
            StartCoroutine(BoardMove());
        }
    }

    IEnumerator BoardMove()
    {
        singleCall_0 = false;

        // �÷��̾� ���� ���� ��ġ�� �̵�
        //if (singleCall_1)
        //{
        //    singleCall_1 = false;
        //    player.transform.position = P_point.position + new Vector3( 0, 1, 0 );
        //    //player.GetComponent<PlayerMove>().inEvent = true;
        //}

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        // ������ ���� Ÿ�̸� ����
        timerXR = x == 1 ? timerXR + Time.deltaTime : 0;
        timerXL = x == -1 ? timerXL + Time.deltaTime : 0;
        timerYU = y == 1 ? timerYU + Time.deltaTime : 0;
        timerYD = y == -1 ? timerYD + Time.deltaTime : 0;
        Debug.Log($"{timerXR},{timerXL},{timerYU},{timerYD}");

        // �ֱٿ� ���� Ÿ�̸� ���� (Ÿ�̸Ӱ� ���� �귶����, 0�� �ƴ� ���)
        timerX = timerXR < timerXL ? (timerXR != 0 ? timerXR : timerXL) : timerXL;
        timerY = timerYU < timerYD ? (timerYU != 0 ? timerYU : timerYD) : timerYD;

        // Ű�Է��� ���� ��
        if ((x != 0 || y != 0) && getKey)
        {
            getKey = false;
            // �ֱٿ� ���� ���� ����
            if (timerX < timerY)
                y = 0;
            else
                x = 0;

            // �÷��̾� ��ġ �ʱ�ȭ
            posX += x == 1 ? 1 : (x == -1 ? -1 : 0);
            posY += y == 1 ? 1 : (y == -1 ? -1 : 0);

            // �÷��̾� �̵�
            for (float f = 0; ; f += Time.deltaTime)
            {
                Debug.Log("for");
                p_rigid.velocity = new Vector2(x * speed * Time.deltaTime, y * speed * Time.deltaTime);
                yield return null;
                if (y == 0 && x * speed * f == 2 * width) break;
                if (x == 0 && y * speed * f == 2 * height) break;
                if (f > 2) break;
            }
            singleCall_0 = true;
            getKey = true;
        }
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
