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
    private float mobSpeed = 5.5f;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private Animator playerAnimator;

    [SerializeField] Blink blink;

    void Start()
    {
        mobAnimator = blackMob.GetComponent<Animator>();
        soundManager = GetComponent<SoundManager>();

        //player = GameObject.Find("Player");
        //playerMove = player.GetComponent<PlayerMove>();
        //playerAnimator = player.GetComponent<Animator>();

        //player = GameObject.FindWithTag("Player");
        //playerMove = player.GetComponent<PlayerMove>();
        //playerAnimator = player.GetComponent<Animator>();

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
        soundManager.PlaySound(1, true, false, 1); // ��ġ�� �Ҹ� ���
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
        soundManager.PlaySound(0, false, true, 1.8f); // �߼Ҹ� ���, ����
        blackMob.SetActive(true);
        mobAnimator.SetFloat("DirX", 1);
        blink.StartBlink("close", 3);
        while (Vector2.Distance(blackMob.transform.position, player.transform.position) > 4)
        {
            blackMob.GetComponent<Rigidbody2D>().velocity = new Vector2(mobSpeed, 0);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        mobAnimator.SetBool("isWalk", false);
        soundManager.StopSound(0);
        // ���ΰ��� ������;
        // �� �ε�;
        

        playerMove.inEvent = false;
        SceneManager.LoadScene("Game_3");
    }
}
