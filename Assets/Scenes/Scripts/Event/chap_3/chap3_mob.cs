using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap3_mob : MonoBehaviour
{
    [SerializeField] private BlackSpace space;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "chap3_player")
        {
            space.ResetPosition();
        }
    }
}
