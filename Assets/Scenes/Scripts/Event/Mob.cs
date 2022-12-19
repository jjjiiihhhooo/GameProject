using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private bool isTrigger = true;
    [SerializeField] private float speed = 1;
    public int count;
    [SerializeField] private bool isStart;
    //[SerializeField] private TestChat testChat;
    [SerializeField] GameObject blackMob;
    [SerializeField] GameObject pivot;
    [SerializeField] GameObject rightWall;
    [SerializeField] GameObject right_arrow;
    [SerializeField] QuestManager questManager;
    Animator ani;
    [SerializeField] CameraManager cm;
    [SerializeField] GameObject potal2;

    [SerializeField] GameObject player;

    [SerializeField] GameObject mobP;

    DialogueBox dialogueBox;
    [SerializeField] MusicPlayer musicPlayer;
    AudioSource audioSource;
    bool spaceDown;
    bool mobReady = false;
    int spaceCount;
    bool bool1 = true;
    bool bool0 = true;
    bool isX;

    float x = 0;
    float y = 0;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueBox = GetComponent<DialogueBox>();
        questManager = FindObjectOfType<QuestManager>();
        //musicPlayer = GameObject.FindWithTag("Canvas").transform.Find("SoundManager").gameObject.GetComponent<MusicPlayer>();

        ani = GetComponent<Animator>();
    }
    public void Count() // �� �������� ��ȭ�� ȣ��
    {
        count++;
        questManager.chap2_questText();
    }
    public IEnumerator Bool()
    {
        cm.TransObj(mobP, 5);
        yield return new WaitForSeconds(0.5f);
        while (cm.isMoving) yield return null;
        isStart = true;
        dialogueBox.SetDialogue();
        //testChat.Chat(); // ??�� ���� ����.
        isTrigger = false;
    }

    IEnumerator Moving()
    {
        cm.TransObj(this.gameObject, 4);
        yield return new WaitForSeconds(0.5f);
        while (cm.isMoving) yield return null;
        ani.SetBool("isWalk", true);
        while (!isTrigger)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.down * speed;
            yield return null;
        }
    }

    private void Update()
    {
        if (!isStart && count > 3 && player.transform.position.x > -48f && player.transform.position.x < -42f && player.transform.position.y > 12f && player.transform.position.x < 16f && bool1) // ��� ��ȭ�� ��ġ�� ���� ���� ���� ���� ��
        {
            bool1 = false;
            player.GetComponent<PlayerMove>().inEvent = true;
            StartCoroutine(Bool());
        }
        if (!isTrigger)
        {
            if (dialogueBox.noMore && bool0) // ??�� ���� ����. ���� space�� ������
            {
                bool0 = false;
                StartCoroutine(Moving());
            }
        }
        if (mobReady)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            spaceCount++;
            if (spaceCount >= 4 && !dialogueBox.isLog)
            {
                mobReady = false;
                audioSource.Play();
                rightWall.SetActive(false);
                right_arrow.SetActive(true);
                questManager.qusetText.SetActive(false);
                StartCoroutine(MobStart()); // ���� ��ü ����
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MobPar")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Animator>().SetFloat("DirX", -1);
            isTrigger = true; // �ش� ������Ʈ �������� ����
            ani.SetBool("isWalk", false);
            cm.TransObj(player, 6);
            other.GetComponent<MobPar>().Chat(); // �ƴ�...�η�����
            mobReady = true;
        }
    }

    private IEnumerator MobStart()
    {
        potal2.SetActive(true);
        blackMob.SetActive(true);
        cm.TransObj(pivot, 4);
        yield return new WaitForSeconds(0.5f);
        while (cm.isMoving) yield return null;
        /*
         * ���� �����ϰ�, �÷��̾ ���� �ٶ�.
         * ������ �Ÿ��� �پ���, ������ �鿩 2�� �ް����� ħ.
         */
        cm.TransObj(player, 6);

        while(Vector2.Distance(blackMob.transform.position, player.transform.position) > 9)
            yield return null;

        // �÷��̾�� ���� ��ü�� ��ġ�� ��������� ���ϰ� �ƴ� �¿��� ��
        if (Mathf.Abs(blackMob.transform.position.x - player.transform.position.x) >= Mathf.Abs(blackMob.transform.position.y - player.transform.position.y))
        {
            isX= true;
            if (blackMob.transform.position.x - player.transform.position.x > 0)
                x = 1;
            else x = -1;
        }
        // �÷��̾�� ���� ��ü�� ��ġ�� ��������� �¿찡 �ƴ� ������ ��
        else
        {
            isX = false;
            if (blackMob.transform.position.y - player.transform.position.y > 0)
                y = 1;
            else y = -1;
        }

        if(isX)
        {
            player.GetComponent<Animator>().SetFloat("DirX", x);
            player.GetComponent<Animator>().SetFloat("DirY", 0);

            for (float i = 0; i < 0.5f; i += Time.deltaTime)
            {
                player.transform.Translate(-x * Time.deltaTime, 0, 0);
                player.GetComponent<Animator>().SetBool("Walk", true);
                yield return null;
            }
            player.GetComponent<Animator>().SetBool("Walk", false);
            yield return new WaitForSeconds(0.3f);
            for (float i = 0; i < 0.5f; i += Time.deltaTime)
            {
                player.transform.Translate(-x * Time.deltaTime, 0, 0);
                player.GetComponent<Animator>().SetBool("Walk", true);
                yield return null;
            }
            player.GetComponent<Animator>().SetBool("Walk", false);
            yield return new WaitForSeconds(0.5f);
            musicPlayer.isRunningGame = true;
            player.GetComponent<PlayerMove>().inEvent = false;
            blackMob.GetComponent<BlackMob_chap2>().moveSpeed = 3.0f;
        }
        else
        {
            player.GetComponent<Animator>().SetFloat("DirY", y);
            player.GetComponent<Animator>().SetFloat("DirX", 0);

            for (float i = 0; i < 0.5f; i += Time.deltaTime)
            {
                player.transform.Translate(0, -y * Time.deltaTime, 0);
                player.GetComponent<Animator>().SetBool("Walk", true);
                yield return null;
            }
            player.GetComponent<Animator>().SetBool("Walk", false);
            yield return new WaitForSeconds(0.3f);
            for (float i = 0; i < 0.5f; i += Time.deltaTime)
            {
                player.transform.Translate(0, -y * Time.deltaTime, 0);
                player.GetComponent<Animator>().SetBool("Walk", true);
                yield return null;
            }
            player.GetComponent<Animator>().SetBool("Walk", false);
            yield return new WaitForSeconds(0.5f);
            musicPlayer.isRunningGame = true;
            player.GetComponent<PlayerMove>().inEvent = false;
            blackMob.GetComponent<BlackMob_chap2>().moveSpeed = 3.0f;
        }
    }
}