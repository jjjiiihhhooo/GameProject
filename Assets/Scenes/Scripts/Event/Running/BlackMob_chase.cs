using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
public class BlackMob_chase : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private ChaseScene chaseScene;
    private Animator animator;
    [SerializeField] Animator tanimator;
    [SerializeField] Tongue tongue;
    private SoundManager soundManager;
    private Vector3 sizeSpeed;
    private int currentLocation = 0;
    private int phase = 1;
    private bool isAction = true;
    private bool isGrow = false;
    private bool singleCall_0 = true;
    private bool singleCall_1 = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
        chaseScene = FindObjectOfType<ChaseScene>();
        sizeSpeed = new Vector3(0.01f, 0.015f, 0);
    }

    private void Update()
    {
        if (singleCall_1)
        {
            soundManager.PlaySound(1, false, true, 1.8f); // 발소리 출력, 루프
            singleCall_1 = false;
        }
        if (chaseScene.isChase) // 추격씬(러닝 게임) 중일 때
        {
            if (singleCall_0)
            {
                singleCall_0 = false;
                StartCoroutine(StartGetGrow());
            }
            if (isGrow)
                //StartCoroutine(GetGrow());

            animator.SetFloat("DirX", 1);
            StartCoroutine(MobPattern(1, 4, 1, true));
            StartCoroutine(MobPattern(2, 4, -1, true));
            StartCoroutine(MobPattern(3, 2, -1, true));
            StartCoroutine(MobPattern(4, 4, 1, true));
            StartCoroutine(MobPattern(5, 0, -1, false));
            StartCoroutine(MobPattern(6, 2, -1, true));
            //StartCoroutine(MobPattern(1, 0, -1, false));
            //StartCoroutine(MobPattern(2, 0, -1, false));
            //StartCoroutine(MobPattern(3, 0, -1, false));
            //StartCoroutine(MobPattern(4, 0, -1, false));
            //StartCoroutine(MobPattern(5, 0, -1, false));
            //StartCoroutine(MobPattern(6, 3, -1, false));

            if (phase == 7)
                chaseScene.EndChase = true;
        }
        else if (!chaseScene.isChase)
            StopCoroutine(GetGrow());
    }

    private IEnumerator StartGetGrow()
    {
        yield return new WaitForSeconds(3.0f);
        isGrow = true;
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
                    if (this.transform.position.y <= chaseScene.mobLocation.y + 0.5f * MoveLocation)
                        break;
                }
            else if (currentLocation < MoveLocation)
                while (true)
                {
                    transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                    yield return new WaitForSeconds(Time.deltaTime);
                    if (this.transform.position.y >= chaseScene.mobLocation.y + 0.75f * MoveLocation)
                        break;
                }

            if (isAttack)
            {
                //tanimator.SetTrigger("isAttack");
                yield return new WaitForSeconds(0.3f);
                tongue.isAttack = true;
                yield return new WaitForSeconds(0.2f);
                soundManager.PlaySound(0, true, false, 0.8f); // 포효소리 출력
                yield return new WaitForSeconds(0.2f);
                tongue.isAttack = false;
                yield return new WaitForSeconds(0.8f); // 공격모션 출력 시간
            }
            phase++;
            isAction = true;
        }
    }
}