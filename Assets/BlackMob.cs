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
    private Vector2 moveDirection; // 플레이어 위치와 몹의 상대 위치를 계산해서 플레이어를 쫓아감.
    private Vector3 sizeSpeed;
    private int currentLocation = 0;
    private int phase = 1;
    private bool isAction = true;
    //public bool canMove = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        chaseScene = FindObjectOfType<ChaseScene>();
        sizeSpeed = new Vector3(0.04f, 0.045f, 0);
    }

    private void Update()
    {
        if (!chaseScene.isChase) // 추격씬(러닝 게임) 중이 아닐 때
        {
            moveDirection.x = (player.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            moveDirection.y = ((player.transform.position.y - 1) - transform.position.y) > 0 ? 1 : -1; // 추후 플레이어의 비벗이 중앙 하단으로 옮기면 -1 삭제

            float pX = Mathf.Floor(player.transform.position.x); // 소수점 아래 생략.
            float mX = Mathf.Floor(transform.position.x);

            // 검은형체 애니메이션 출력
            if (pX > mX)
                animator.SetFloat("DirX", 1);
            else if (pX < mX)
                animator.SetFloat("DirX", -1);

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime); // 플레이어 추격
        }
        else if (chaseScene.isChase) // 추격씬(러닝 게임) 중일 때
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
    private IEnumerator MobPattern(int _phase, float StandBy, int MoveLocation, bool isAttack) // 인자들은 각각 순번, 대기시간, 이동위치(-1, 0, 1까지 아래, 중앙, 위), 공격 여부를 나타냄.
    {
        if ((phase == _phase) && isAction)
        {
            isAction = false;

            // 딜레이
            yield return new WaitForSeconds(StandBy);

            // 이동
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
