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

    // chap3���� ������ ���� ������
    public bool chap3JY;
    // chap4���� �������� ������ �˸°� �����
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