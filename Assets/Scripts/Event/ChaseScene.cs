using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject BlackMob;
    [SerializeField] GameObject black; // �ʱ� ȭ�� ������ ���� �κ�. �÷��̾ ȭ�� ���ʿ������� �����̴� ����
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"����"�� �̹����� ���� ������Ʈ �迭
    [SerializeField] Transform mapTransform; // �� ��ġ ��ǥ
    Animator chaseAnimator;
    int length;

    public bool isChase = false;

    float width = 12.8f; // "����"�� x��

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // ����� ����ġ�� �ӵ�. ����� �÷��̾� x�� �̵� �ӵ�
    public float ySpeed = 10; // �÷��̾��� y�� �̵� �ӵ�

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
        if (isChase) // isChase���� ���� MoveChase() ���� �Ǵ� �ߴ�
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
         * �÷��̾��� ��쿡�� �ִϸ��̼��� ����ϸ鼭 ��ġ�� y�� �̵��� �Ѵ�. 
         * ����� �Է��� horizontal���� ���� x������ �����̾� ��Ʈ ���ó�� �̵��Ѵ�.
         * �̿� ���� ��������� �÷��̾ ��濡 ���� x������ �����̴� �� ���� ���δ�.
         */

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

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
}
