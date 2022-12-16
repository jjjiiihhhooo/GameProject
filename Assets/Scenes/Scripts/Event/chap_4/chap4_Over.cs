using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_Over : MonoBehaviour
{
    [SerializeField] GameObject player;
    GameObject canvas;
    ActiveGameOver gameOver;

    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        gameOver = canvas.GetComponent<ActiveGameOver>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameOver.SetActive(true);
            player.GetComponent<PlayerMove>().inEvent = false;
        }
    }
}
