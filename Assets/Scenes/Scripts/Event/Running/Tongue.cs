using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public bool isAttack;
    bool single;
    [SerializeField] private ActiveGameOver gameOver;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && isAttack && !single)
        {
            single = true;
            gameOver.SetActive(true);
        }
    }
}
