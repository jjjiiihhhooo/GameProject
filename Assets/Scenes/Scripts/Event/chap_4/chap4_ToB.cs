using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_ToB : MonoBehaviour
{
    // �÷��̾ �����ϸ� true�� ��ȯ
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
