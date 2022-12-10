using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private GameObject mainCamera;

    [SerializeField] GameObject BlackMob;
    [SerializeField] GameObject black; // �ʱ� ȭ�� ������ ���� �κ�. �÷��̾ ȭ�� ���ʿ������� �����̴� ����
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"����"�� �̹����� ���� ������Ʈ �迭
    [SerializeField] Transform mapTransform; // �� ��ġ ��ǥ
    [SerializeField] PushScene pushScene;
    Animator chaseAnimator;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject fade;
    int length;

    public bool isChase = false;
    public bool EndChase = false;
    private bool singleCall_0 = true;
    private bool singleCall_1 = true;

    float width = 12.8f; // "����"�� x��

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // ����� ����ġ�� �ӵ�. ����� �÷��̾� x�� �̵� �ӵ�
    public float ySpeed = 10; // �÷��̾��� y�� �̵� �ӵ�

    public Vector3 mobLocation;
    //public Vector3 cameraLocation;

    PlayerMove PM;

    void Start()
    {

        //player = GameObject.Find("Player");
        //Debug.Log(player.name);

        if(player == null)
            player = GameObject.Find("Player");

        PM = player.GetComponent<PlayerMove>();
        mainCamera = GameObject.FindWithTag("MainCamera");
        chaseAnimator = player.GetComponent<Animator>();

        isChase = true;
        mainCamera.GetComponent<CameraManager>().isChase = true;

        PM.inEvent = true;
        //cameraLocation = mapTransform.position + new Vector3(6, 2, -1);
        mobLocation = mapTransform.position + new Vector3(-9, -1.5f, 0);
        length = backGround.Length;

        
        if (canvas == null)
            canvas = GameObject.FindWithTag("Canvas");
        if(fade == null)
            fade = GameObject.Find("Canvas").transform.Find("Fade").gameObject;
    }

    void Update()
    {
        if (isChase)
        {
            StartCoroutine(MoveChase());
            if (EndChase && singleCall_0)
            {
                singleCall_0 = false;
                StartCoroutine(ExitChase());
            }
        }
        else if (!isChase && singleCall_1)
        {
            Debug.Log("pp");
            singleCall_1 = false;
            pushScene.EndOfAlley();
        }
    }

    private IEnumerator MoveChase()
    {
        /*
         * �÷��̾��� ��쿡�� �ִϸ��̼��� ����ϸ鼭 ��ġ�� y�� �̵��� �Ѵ�. 
         * ����� �Է��� horizontal���� ���� x������ �����̾� ��Ʈ ���ó�� �̵��Ѵ�.
         * �̿� ���� ��������� �÷��̾ ��濡 ���� x������ �����̴� �� ���� ���δ�.
         */

        horizontal = EndChase? 1 :Input.GetAxisRaw("Horizontal");
        vertical = EndChase ? 0 : Input.GetAxisRaw("Vertical");


        if (EndChase) // �÷��̾ y�� �߾ӿ� ��ġ, Ű �Է°� ����
        {
            if (player.transform.position.y > mapTransform.position.y)
                while (isChase)
                {
                    player.transform.Translate(0, -Time.deltaTime, 0);
                    yield return null;
                    if (player.transform.position.y <= mapTransform.position.y)
                        break;
                }
            else if (player.transform.position.y < mapTransform.position.y)
                while (isChase)
                {
                    player.transform.Translate(0, Time.deltaTime, 0);
                    yield return null;
                    if (player.transform.position.y >= mapTransform.position.y)
                        break;
                }
        }

        // �÷��̾�, �Է��� vertical���� ���� y���� �̵�
        player.transform.Translate(0, vertical * xSpeed * Time.deltaTime, 0);

        // ī�޶�, �Է��� horizontal���� ���� x���� �̵�. ȭ��� ������ 1/3 ������ �÷��̾� ��ġ
        if (mainCamera.transform.position.x >= mapTransform.position.x - 4)
            mainCamera.transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

        // �ִϸ��̼�, horizontal���� ���ؼ� ���
        if (horizontal != 0)
            chaseAnimator.SetFloat("DirX", horizontal);
        chaseAnimator.SetBool("Walk", horizontal == 0 ? false : true);

        // ������ü, �Է��� horizontal���� ���� x���� �̵�
        BlackMob.transform.Translate((1 - horizontal) * xSpeed * Time.deltaTime, 0, 0);

        // ���� ���, �Է��� horizontal���� ���� x���� �̵�
        if (black.transform.position.x >= mapTransform.position.x - 1.8f * width)
            black.transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);
        for (int i = 0; i < length; i++)
        {
            backGround[i].transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

            if (backGround[i].transform.position.x <= mapTransform.position.x - (2.2f * width) + 0.1f) // backGround[i]�� ī�޶󿡼� ������ ���� ������ �з����� ��
            {
                float newX = mapTransform.position.x + (1.3f * width) - 0.2f;

                backGround[i].transform.position = new Vector3(newX, this.transform.position.y, 0); // �ش� ������Ʈ�� ȭ�� �����ʿ� �ű�
            }
        }
        yield return null;
    }

    private IEnumerator ExitChase()
    {
        yield return new WaitForSeconds(4.0f);
        
        // �÷��̾� ȭ�� ������ �̵�
        for (float i = 0; i < 1.0f; i += Time.deltaTime)
        {
            player.transform.Translate(ySpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        fade.SetActive(true);
        isChase = false;
        BlackMob.SetActive(false);
        StopCoroutine(MoveChase());
        yield return new WaitForSeconds(0.3f);
    }
}
