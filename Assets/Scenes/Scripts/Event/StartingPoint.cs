using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    GameObject player;
    SceneChanger sceneChanger;
    public string startPoint;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneChanger = FindObjectOfType<SceneChanger>();

        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = this.gameObject.transform.position;
        }
    }
}
