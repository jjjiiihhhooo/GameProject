using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_ToB : MonoBehaviour
{
    // 플레이어가 도착하면 true값 반환
    bool isFinish;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isFinish = true;
        }
    }

    public bool Finish
    {
        get{ return isFinish; }
    }
}
