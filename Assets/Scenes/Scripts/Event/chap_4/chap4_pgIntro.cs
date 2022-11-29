using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_pgIntro : MonoBehaviour
{
    [SerializeField] GameObject JY;
    GameObject player;

    float speed = 1.0f;

    
    // 하나와 지연이 놀이터 중앙으로 간 이후, 지연이 대사를 읊는 장면
    

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        StartCoroutine(PlaygroundIntro());
    }

    void Update()
    {
        
    }

    IEnumerator PlaygroundIntro()
    {
        // 하나, 지연 위로 이동
        player.GetComponent<Animator>().SetFloat("DirX", 0);
        player.GetComponent<Animator>().SetFloat("DirY", 1);
        player.GetComponent<Animator>().SetBool("Walk", true);

        JY.GetComponent<Animator>().SetFloat("DirY", 1);
        JY.GetComponent<Animator>().SetBool("isWalk", true);

        while (player.transform.position.y < 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.up * speed * Time.deltaTime;
            JY.GetComponent<Rigidbody2D>().velocity = Vector3.up * speed * Time.deltaTime;
            yield return null;
        }

        player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        JY.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // 서로 마주보고 대사 출력
        player.GetComponent<Animator>().SetBool("Walk", false);
        JY.GetComponent<Animator>().SetBool("isWalk", false);

        player.GetComponent<Animator>().SetFloat("DirY", 0);
        player.GetComponent<Animator>().SetFloat("DirX", 1);
        JY.GetComponent<Animator>().SetFloat("DirY", 0);
        JY.GetComponent<Animator>().SetFloat("DirX", -1);

        yield return new WaitForSeconds(0.5f);

        JY.GetComponent<DialogueBox>().SetDialogue();
        player.GetComponent<PlayerMove>().inEvent = false;
    }
}
