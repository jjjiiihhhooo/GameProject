using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

    //[SerializeField] GameObject player;
    //[SerializeField] GameObject mainCamera;

    public string currentScene = "Main";
    [SerializeField] bool DontDestroy;

    // chap3에서 지연과 같이 갔는지
    public bool chap3JY;
    // chap4에서 지연에게 쪽지를 알맞게 줬는지
    public bool chap4JY;

    void Start()
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
}