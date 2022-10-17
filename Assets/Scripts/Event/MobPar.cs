using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPar : MonoBehaviour
{
    [SerializeField] private TestChat testChat;


    public void Chat()
    {
        testChat.Chat();
    }
}
