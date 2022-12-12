using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    [SerializeField] GameObject puzzle;
    [SerializeField] Blink blink;

    void Start()
    {
        //blink.StartBlink("close", 3);
        puzzle.SetActive(true);
    }
}
