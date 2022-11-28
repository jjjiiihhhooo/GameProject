using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    public bool isGameOver;
    [SerializeField] GameObject gameOver;

    private void Awake()
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

    //private void Update()
    //{
    //    if (isGameOver)
    //    {
    //        gameOver.SetActive(true);
    //    }
    //}

    public GameObject GetChild(string name)
    {
        return GameObject.Find(name);
    }
}
