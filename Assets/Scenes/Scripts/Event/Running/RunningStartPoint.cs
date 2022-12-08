
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStartPoint : MonoBehaviour
{
    public string startPoint;
    private SceneChanger sceneChanger;
    private GameObject fade;
    private GameObject player;
    private GameObject mainCamera;

    void Start()
    {
        fade = GameObject.FindWithTag("Canvas").transform.Find("Fade").gameObject;
        fade.SetActive(true);

        sceneChanger = FindObjectOfType<SceneChanger>();
        player = GameObject.Find("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");

        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = new Vector3(-1, -1.5f, 0);
            mainCamera.transform.position = this.transform.position + new Vector3(6, 2, -1);
            Debug.Log($"{player.transform.position.x}/{player.transform.position.y}/{player.transform.position.z}");
        }
    }
}