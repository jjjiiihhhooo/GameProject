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

    [SerializeField] GameObject player;
    
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
    }
    public void Count() // 각 지연과의 대화로 호출
    {
        count++;
        questManager.chap2_questText();
    }
    public void Bool()
    {
        isStart = true;
        dialogueBox.SetDialogue();
        //testChat.Chat(); // ??아 집에 가자.
        isTrigger = false;
    }

    IEnumerator Moving()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        yield return null;
    }

    private void Update()
    {
        if (!isStart && count > 3 && player.transform.position.x > -50f && player.transform.position.x < -40f && player.transform.position.y > 9f && player.transform.position.x < 17f) // 모든 대화를 마치고 일정 영역 내에 있을 때
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
                StartCoroutine(MobStart()); // 검은 형체 등장
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MobPar")
        {
            player.GetComponent<Animator>().SetFloat("DirX", -1);
            isTrigger = true; // 해당 오브젝트 움직임을 멈춤
            other.GetComponent<MobPar>().Chat(); // 아니...부러워서
            mobReady = true;
        }
    }

    private IEnumerator MobStart()
    {
        yield return new WaitForSeconds(0.5f);
        /*
         * 몹이 등장하고, 플레이어가 몹을 바라봄.
         * 몹과의 거리가 줄어들면, 간격을 들여 2번 뒷걸음질 침.
         */
        blackMob.SetActive(true);
        player.GetComponent<Animator>().SetFloat("DirX", -1);
        while(Vector2.Distance(blackMob.transform.position, player.transform.position) > 8)
            yield return null;
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