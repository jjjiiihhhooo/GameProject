using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private SoundManager soundManager;
    public bool isRunningGame = false;
    private bool singleCall_0 = true;

    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    void Update()
    {
        if (isRunningGame && singleCall_0)
        {
            singleCall_0 = false;
            soundManager.PlaySound(0, false, true);
        }
    }
}
