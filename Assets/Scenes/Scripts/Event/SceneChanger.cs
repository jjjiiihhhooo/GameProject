using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;

    public string currentScene = "Main";

    // chap3���� ������ ���� ������
    public bool chap3JY;
    // chap4���� �������� ������ �˸°� �����
    public bool chap4JY;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");
    }
}