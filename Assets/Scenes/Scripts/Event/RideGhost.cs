using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideGhost : MonoBehaviour
{
    [SerializeField] private Mob mob;

    // ��ȭ ���Ῡ��
    bool LogEnd;
    bool onCount = true;

    void Update()
    {
        LogEnd = GetComponent<DialogueBox>().isEnd;

        if (LogEnd && onCount)
        {
            onCount = false;
            mob.Count();
        }
    }
}
