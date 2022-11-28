using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_Eye : MonoBehaviour
{
    [SerializeField] GameObject white; // ÈòÀÚ
    [SerializeField] GameObject black; // °ËÀºÀÚ
    [SerializeField] GameObject eyelid; // ´«²¨Ç®
    GameObject player; 
    GameObject canvas;
    ActiveGameOver gameOver;
    public bool isOpen = false;
    bool open;
    bool single;

    Transform playerPos;
    Vector3 localPos;
    Vector3 direction;
    float distance;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        localPos = this.transform.position;
        canvas = GameObject.FindWithTag("Canvas");
        gameOver = canvas.GetComponent<ActiveGameOver>();
        // new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    void Update()
    {

        if (isOpen)
        {
            if (!single)
            {
                single = true;
                StartCoroutine(EyeMove());
            }
        }
        else if (!isOpen)
        {
            StopAllCoroutines();
            single = false;

            open = false;

            eyelid.SetActive(true);
            white.SetActive(false);
            black.SetActive(false);
        }
    }

    IEnumerator EyeMove()
    {
        if (!open)
        {
            eyelid.SetActive(false);
            white.SetActive(true);
            black.SetActive(true);

            open = true;
        }

        playerPos = GameObject.FindWithTag("Player").transform;
        direction = playerPos.position - this.white.transform.position;
        distance = Vector2.Distance(this.gameObject.transform.position, playerPos.position);

        if (distance >= 0.1f)
        {
            direction *= 0.1f / distance;
        }

        this.black.transform.position = Vector3.MoveTowards(this.black.transform.position, localPos + direction + new Vector3(0, 0, -0.1f), 2 * Time.deltaTime);
        yield return null;

        single = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "c4_p1" && isOpen)
        {
            gameOver.SetActive(true);
            player.GetComponent<PlayerMove>().inEvent = false;
        }
    }
}
