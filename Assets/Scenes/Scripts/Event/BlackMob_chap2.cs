using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BlackMob_chap2 : MonoBehaviour
{
    [SerializeField] public float moveSpeed;
    [SerializeField] Transform player;
    private Animator animator;
    private Vector2 moveDirection; // 플레이어 위치와 몹의 상대 위치를 계산해서 플레이어를 쫓아감.
    private SoundManager soundManager;
    //private bool singleCall_0 = true;
    private bool singleCall_1 = true;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
    }

    private void Update()
    {
        if (singleCall_1)
        {
            soundManager.PlaySound(0, false, true, 1.8f); // 발소리 출력, 루프
            singleCall_1 = false;
        }
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
}