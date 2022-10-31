using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PushPlayer : MonoBehaviour
{
    [SerializeField] GameObject blackMob;
    private Animator mobAnimator;
    private SoundManager soundManager;
    private float mobSpeed = 5.0f;
    private GameObject player;
    private PlayerMove playerMove;
    private Animator playerAnimator;

    void Start()
    {
        mobAnimator = blackMob.GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();
        player = GameObject.FindWithTag("Player");
        playerMove = player.GetComponent<PlayerMove>();
        playerAnimator = player.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) // �����̿��� �ٰ�����
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ActPush()); 
        }
    }

    private IEnumerator ActPush()
    {
        // �����̰� �÷��̾ ��ħ
        playerMove.inEvent = true;
        playerAnimator.SetFloat("DirX", 1);
        soundManager.PlaySound(1, true, false); // ��ġ�� �Ҹ� ���
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

        // ���� ��ü ����
        soundManager.PlaySound(0, false, true); // �߼Ҹ� ���, ����
        blackMob.SetActive(true);
        mobAnimator.SetFloat("DirX", 1);
        while (Vector2.Distance(blackMob.transform.position, player.transform.position) > 8)
        {
            blackMob.transform.Translate(mobSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }
        mobAnimator.SetBool("isWalk", false);
        soundManager.StopSound();
        // ���ΰ��� ������;
        // �� �ε�;

        playerMove.inEvent = false;
        Debug.Log("Done.");
    }
}
