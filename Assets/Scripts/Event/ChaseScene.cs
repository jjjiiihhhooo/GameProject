using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject BlackMob;
    [SerializeField] GameObject black; // �ʱ� ȭ�� ������ ���� �κ�. �÷��̾ ȭ�� ���ʿ������� �����̴� ����
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"����"�� �̹����� ���� ������Ʈ �迭
    [SerializeField] Transform mapTransform; // �� ��ġ, �ش� �� ī�޶� ��ġ�� ��ǥ
    Animator chaseAnimator;
    int length;

    public bool isChase = false;

    float width = 12.8f; // "����"�� x��

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // ����� ����ġ�� �ӵ�. ����� �÷��̾� x�� �̵� �ӵ�
    public float ySpeed = 10; // �÷��̾��� y�� �̵� �ӵ�

    public Vector3 mobLocation;

    void Start()
    {
        mobLocation = mapTransform.position + new Vector3(-7, -0.5f, 0);
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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // �Է��� vertical���� ���� �÷��̾��� y���� �̵�
        player.transform.Translate(0, vertical * xSpeed * Time.deltaTime, 0);

        // horizontal���� ���ؼ� �ִϸ��̼Ǹ� ���
        if (horizontal != 0)
            chaseAnimator.SetFloat("DirX", horizontal);
        chaseAnimator.SetBool("Walk", horizontal == 0 ? false : true);
        
        // �Է��� horizontal���� ���� ������ü�� x���� �̵�
        BlackMob.transform.Translate((1 - horizontal) * xSpeed * Time.deltaTime, 0, 0);

        // �Է��� horizontal���� ���� ���� ����� x���� �̵�
        if (black.transform.position.x >= mapTransform.position.x - width - 2)
            black.transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

        for (int i = 0; i < length; i++)
        {
            backGround[i].transform.Translate(-horizontal * ySpeed * Time.deltaTime, 0, 0);

            if (backGround[i].transform.position.x <= mapTransform.position.x - width - 2) // backGround[i]�� ī�޶󿡼� ������ ���� ������ �з����� ��
            {
                backGround[i].transform.Translate(mapTransform.position.x + width - 2, 0, 0); // �ش� ������Ʈ�� ȭ�� �����ʿ� �ű�
            }
        }
        yield return null;
    }
}
