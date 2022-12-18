using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] public bool DontDestroy;
    public static MusicPlayer instance;

    private SoundManager soundManager;
    public bool isRunningGame = false;
    private bool singleCall_0 = true;

    private void Awake()
    {
        if (DontDestroy)
        {
            if (instance == null)
            {
                DontDestroyOnLoad(this.gameObject);
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    void Start()
    {
        soundManager = GetComponent<SoundManager>();
    }

    void Update()
    {
        if (isRunningGame && singleCall_0)
        {
            singleCall_0 = false;
            soundManager.PlaySound(0, false, true, 1);
        }
    }
}
