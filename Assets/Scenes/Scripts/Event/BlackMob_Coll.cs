using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMob_Coll : MonoBehaviour
{
    [SerializeField] private ActiveGameOver gameOver;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            gameOver.SetActive(true);
        }
    }
}
