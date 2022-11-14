using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SoundManager : MonoBehaviour
{
    AudioSource[] audioSource;

    void Awake()
    {
        audioSource = GetComponents<AudioSource>();
    }

    public void PlaySound(int soundNum, bool isOneShot, bool isLoop, float pitch)
    {
        if (isOneShot)
        {
            audioSource[soundNum].pitch = pitch;
            audioSource[soundNum].PlayOneShot(audioSource[soundNum].clip);
        }
        else if (!isOneShot)
        {
            audioSource[soundNum].loop = isLoop;
            audioSource[soundNum].pitch = pitch;
            audioSource[soundNum].Play();
        }
    }

    public void StopSound(int soundNum)
    {
        if (audioSource[soundNum].clip == null)
            return;
        audioSource[soundNum].Stop();
    }
}
