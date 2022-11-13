using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClip;
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int soundNum, bool isOneShot, bool isLoop, float pitch)
    {
        if (isOneShot)
        {
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip[soundNum]);
        }
        else if (!isOneShot)
        {
            audioSource.clip = audioClip[soundNum];
            audioSource.loop = isLoop;
            audioSource.pitch = pitch;
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource.clip == null)
            return;
        audioSource.Stop();
    }
}
