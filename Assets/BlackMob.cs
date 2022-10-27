using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlackMob : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] Transform player;
    private ChaseScene chaseScene;
    private Animator animator;
    private Vector2 moveDirection; // �÷��̾� ��ġ�� ���� ��� ��ġ�� ����ؼ� �÷��̾ �Ѿư�.
    private Vector3 sizeSpeed;
    private int currentLocation = 0;
    private int phase = 1;
    private bool isAction = true;
    //public bool canMove = false;

    private void Awake()
    {
        Invoke("IsStart", 1f);
        sizeSpeed = new Vector3(0.1f, 0.15f, 0);
    }

    private void IsStart()
    {
        isStart = true; 
        animator = GetComponent<Animator>();
        chaseScene = FindObjectOfType<ChaseScene>();
        sizeSpeed = new Vector3(0.04f, 0.045f, 0); 
    }

    private void Update()
    {
        if (!chaseScene.isChase) // �߰ݾ�(���� ����) ���� �ƴ� ��
        {
            moveDirection.x = (player.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            moveDirection.y = ((player.transform.position.y - 1) - transform.position.y) > 0 ? 1 : -1; // ���� �÷��̾��� ����� �߾� �ϴ����� �ű�� -1 ����

            float pX = Mathf.Floor(player.transform.position.x); // �Ҽ��� �Ʒ� ����.
            float mX = Mathf.Floor(transform.position.x);

            // ������ü �ִϸ��̼� ���
            if (pX > mX)
                animator.SetFloat("DirX", 1);
            else if (pX < mX)
                animator.SetFloat("DirX", -1);

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // �÷��̾� �߰�
        }
        else if (chaseScene.isChase) // �߰ݾ�(���� ����) ���� ��
        {
            Invoke("StartGetGrow", 3.0f);

            if (chaseScene.isChase)
            {
                animator.SetFloat("DirX", 1);
                StartCoroutine(MobPattern(1, 4, 1, true));
                StartCoroutine(MobPattern(2, 4, -1, true));
                StartCoroutine(MobPattern(3, 2, -1, true));
                StartCoroutine(MobPattern(4, 4, 1, true));
                StartCoroutine(MobPattern(5, 2, -1, false));
                StartCoroutine(MobPattern(6, 3, -1, true));
            }
        }
    }

    void StartGetGrow()
    {
        StartCoroutine(GetGrow());
    }
    private IEnumerator GetGrow()
    {
        this.transform.localScale += sizeSpeed * Time.deltaTime;
        yield return null;
    }
    private IEnumerator MobPattern(int _phase, float StandBy, int MoveLocation, bool isAttack) // ���ڵ��� ���� ����, ���ð�, �̵���ġ(-1, 0, 1���� �Ʒ�, �߾�, ��), ���� ���θ� ��Ÿ��.
    {
        if ((phase == _phase) && isAction)
        {
            isAction = false;

            // ������
            yield return new WaitForSeconds(StandBy);

            // �̵�
            if (currentLocation > MoveLocation)
                while (true)
                {
                    transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                    yield return new WaitForSeconds(Time.deltaTime);

                    if (this.transform.position.y <= chaseScene.mobLocation.y + 0.75 * MoveLocation)
                        break;
                }
            else if (currentLocation < MoveLocation)
                while (true)
                {
                    transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                    yield return new WaitForSeconds(Time.deltaTime);

                    if (this.transform.position.y >= chaseScene.mobLocation.y + 0.75 * MoveLocation)
                        break;
                }
            
            if (isAttack)
            {
                animator.SetTrigger("isAttack");
            }
            phase++;
            isAction = true;
        }
    }
}
