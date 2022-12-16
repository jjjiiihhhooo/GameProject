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
    [SerializeField] private GameObject blackMob;
    [SerializeField] private GameObject rightWall;
    [SerializeField] private GameObject right_arrow;
    [SerializeField] private QuestManager questManager;
    Animator ani;
    [SerializeField] CameraManager cm;

    [SerializeField] GameObject player;

    [SerializeField] GameObject mobP;

    DialogueBox dialogueBox;
    MusicPlayer musicPlayer;
    AudioSource audioSource;
    bool spaceDown;
    bool mobReady = false;
    int spaceCount;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueBox = GetComponent<DialogueBox>();
        questManager = FindObjectOfType<QuestManager>();
        musicPlayer = GameObject.FindWithTag("Canvas").transform.Find("SoundManager").gameObject.GetComponent<MusicPlayer>();

        ani = GetComponent<Animator>();
    }
    public void Count() // �� �������� ��ȭ�� ȣ��
    {
        count++;
        questManager.chap2_questText();
    }
    public void Bool()
    {
        isStart = true;
        dialogueBox.SetDialogue();
        //testChat.Chat(); // ??�� ���� ����.
        isTrigger = false;
    }

    IEnumerator Moving()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
        ani.SetBool("isWalk", true);
        yield return null;
    }

    private void Update()
    {
        if (!isStart && count > 3 && player.transform.position.x > -50f && player.transform.position.x < -40f && player.transform.position.y > 9f && player.transform.position.x < 17f) // ��� ��ȭ�� ��ġ�� ���� ���� ���� ���� ��
        {
            player.GetComponent<PlayerMove>().inEvent = true;
            cm.target_obj = mobP;
            Bool();
        }
        if (!isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                spaceDown = true;

            if (spaceDown) // ??�� ���� ����. ���� space�� ������
            {
                cm.target_obj = this.gameObject;
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
            player.GetComponent<Animator>().SetFloat("DirX", -1);
            isTrigger = true; // �ش� ������Ʈ �������� ����
            ani.SetBool("isWalk", false);
            cm.target_obj = player;
            other.GetComponent<MobPar>().Chat(); // �ƴ�...�η�����
            mobReady = true;
        }
    }

    private IEnumerator MobStart()
    {
        cm.target_obj = blackMob;
        yield return new WaitForSeconds(0.5f);
        /*
         * ���� �����ϰ�, �÷��̾ ���� �ٶ�.
         * ������ �Ÿ��� �پ���, ������ �鿩 2�� �ް����� ħ.
         */
        blackMob.SetActive(true);
        player.GetComponent<Animator>().SetFloat("DirX", -1);
        while(Vector2.Distance(blackMob.transform.position, player.transform.position) > 8)
            yield return null;
        cm.target_obj = player;
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            player.transform.Translate(Time.deltaTime, 0, 0);
            player.GetComponent<Animator>().SetBool("Walk", true);
            yield return null;
        }
        player.GetComponent<Animator>().SetBool("Walk", false);
        yield return new WaitForSeconds(0.3f);
        for (float i = 0; i < 0.5f; i += Time.deltaTime)
        {
            player.transform.Translate(Time.deltaTime, 0, 0);
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