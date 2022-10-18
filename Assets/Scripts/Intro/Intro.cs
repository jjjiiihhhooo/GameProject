using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Image background;
    private float fadeCheck = 1;
    private bool isStart;
    private void Awake()
    {
        Invoke("IsStart", 2f);
    }

    private void IsStart()
    {
        isStart = true;
    }

    private void Update()
    {
        if(isStart && fadeCheck > 0.005f)
        {
            DeleteBG();
        }
        else if(isStart)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void DeleteBG()
    {
        fadeCheck -= 0.01f;
        background.color = new Color(1, 1, 1, fadeCheck);
    }
}
