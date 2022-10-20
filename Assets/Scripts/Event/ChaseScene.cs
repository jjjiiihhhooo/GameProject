using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseScene : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject black; // �ʱ� ȭ�� ������ ���� �κ�. �÷��̾ ȭ�� ���ʿ������� �����̴� ����
    [SerializeField] GameObject[] backGround = new GameObject[3]; //"����"�� �̹����� ���� ������Ʈ �迭
    [SerializeField] Transform scenePosition; // �ش� �� ī�޶� ��ġ�� ��ǥ
    Animator chaseAnimator;
    int length;
    
    public bool isChase = false;

    float width = 12.8f; // "����"�� x��

    float horizontal = 0;
    float vertical = 0;
    public float xSpeed = 10; // ����� ����ġ�� �ӵ�. ����� �÷��̾� x�� �̵� �ӵ�
    public float ySpeed = 10; // �÷��̾��� y�� �̵� �ӵ�

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

        // horizontal���� ���ؼ� �ִϸ��̼Ǹ� ���
        if(horizontal != 0)
            chaseAnimator.SetFloat("DirX", horizontal);
        chaseAnimator.SetBool("Walk", horizontal == 0? false : true);

        // �Է��� vertical���� ���� �÷��̾��� y���� �̵�
        player.transform.Translate(0, vertical * xSpeed * Time.deltaTime, 0);

        //�Է��� horizontal���� ���� ���� ����� x���� �̵�
        if (black.transform.position.x >= scenePosition.position.x - width - 1)
            black.transform.Translate(- horizontal * ySpeed * Time.deltaTime, 0, 0);

        for (int i = 0; i < length; i++)
        {
            backGround[i].transform.Translate(- horizontal * ySpeed * Time.deltaTime, 0, 0);

            if (backGround[i].transform.position.x <= scenePosition.position.x - width - 1) // backGround[i]�� ī�޶󿡼� ������ ���� ������ �з����� ��
            {
                backGround[i].transform.Translate(scenePosition.position.x + width - 1.1f, 0, 0); // �ش� ������Ʈ�� ȭ�� �����ʿ� �ű�
            }
        }
        yield return null;
    }
}
