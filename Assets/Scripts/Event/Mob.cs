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

    public void Count() // �� �������� ��ȭ�� ȣ��
    {
        count++;
    }
    public void Bool()
    {
        isStart = true;
        testChat.Chat(); // ??�� ���� ����.
        isTrigger = false;
    }

    IEnumerator Moving()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        yield return null;
    }

    private void Update()
    {
        if (!isStart && count > 3) // ��� ��ȭ�� ��ġ��
        {
            player.GetComponent<PlayerMove>().inEvent = true;
            Bool();
        }
        if (!isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                spaceDown = true;

            if (spaceDown) // ??�� ���� ����. ���� space�� ������
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
            isTrigger = true; // �ش� ������Ʈ �������� ����
            mobReady = true;
            other.GetComponent<MobPar>().Chat(); // �ƴ�...�η�����
        }
    }

    private void MobStart()
    {
        blackMob.SetActive(true);
    }


}