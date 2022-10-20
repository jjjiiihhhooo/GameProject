using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private bool isTrigger = true;
    [SerializeField] private float speed = 1;
    public int count;
    [SerializeField] private bool isStart;
    [SerializeField] private TestChat testChat;
    [SerializeField] private GameObject blackMob;

    [SerializeField] GameObject player; ///
    bool spaceDown;
    bool mobReady = false;
    int spaceCount = 0;

    public void Count() // 각 지연과의 대화로 호출
    {
        count++;
    }
    public void Bool()
    {
        isStart = true;
        testChat.Chat(); // ??아 집에 가자.
        isTrigger = false;
    }

    IEnumerator Moving()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        yield return null;
    }

    private void Update()
    {
        if (!isStart && count > 3) // 모든 대화를 마치면
        {
            player.GetComponent<PlayerMove>().inEvent = true;
            Bool();
        }
        if (!isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                spaceDown = true;

            if (spaceDown) // ??아 집에 가자. 에서 space를 누르면
                StartCoroutine(Moving());
        }
        if (mobReady && Input.GetKeyDown(KeyCode.Space))
            spaceCount++;
        if (spaceCount >= 4)
        {
            MobStart();
            player.GetComponent<PlayerMove>().inEvent = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MobPar")
        {
            isTrigger = true; // 해당 오브젝트 움직임을 멈춤
            mobReady = true;
            other.GetComponent<MobPar>().Chat(); // 아니...부러워서
        }
    }

    private void MobStart()
    {
        blackMob.SetActive(true);
    }


}