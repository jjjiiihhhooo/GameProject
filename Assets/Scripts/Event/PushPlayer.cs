using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PushPlayer : MonoBehaviour
{
    [SerializeField] GameObject blackMob;
    private Animator mobAnimator;
    private float mobSpeed = 5.0f;
    private GameObject player;
    private PlayerMove playerMove;
    private Animator playerAnimator;

    void Start()
    {
        mobAnimator = blackMob.GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) // 지연이에게 다가가면
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ActPush()); 
        }
    }

    private IEnumerator ActPush()
    {
        // 지연이가 플레이어를 밀침
        playerMove.inEvent = true;
        playerAnimator.SetFloat("DirX", 1);

        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            player.transform.Translate(-Time.deltaTime, 0, 0);
            yield return null;
        }
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            player.transform.Translate(-Time.deltaTime, 0, 0);
            playerAnimator.SetBool("Walk", true);
            yield return null;
        }
        playerAnimator.SetBool("Walk", false);
        yield return new WaitForSeconds(1.5f);

        // 검은 형체 등장
        blackMob.SetActive(true);
        mobAnimator.SetFloat("DirX", 1);
        while (Vector2.Distance(blackMob.transform.position, player.transform.position) > 8)
        {
            blackMob.transform.Translate(mobSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        mobAnimator.SetBool("isWalk", false);
        // 주인공에 혀감기;
        // 씬 로드;

        playerMove.inEvent = false;
        Debug.Log("Done.");
    }
}
