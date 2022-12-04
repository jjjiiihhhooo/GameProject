using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;

    public string currentScene = "Main";

    // chap3에서 지연과 같이 갔는지
    public bool chap3JY;
    // chap4에서 지연에게 쪽지를 알맞게 줬는지
    public bool chap4JY;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
}