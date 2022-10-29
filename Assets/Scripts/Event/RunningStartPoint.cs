
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStartPoint : MonoBehaviour
{
    [SerializeField] ChaseScene chaseScene;
    public string startPoint;
    private SceneChanger sceneChanger;
    private GameObject canvas;
    private GameObject fade;
    private GameObject player;
    private GameObject mainCamera;

    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas");
        fade = GameObject.Find("Canvas").transform.Find("Fade").gameObject;
        fade.SetActive(true);

        sceneChanger = FindObjectOfType<SceneChanger>();
        chaseScene = FindObjectOfType<ChaseScene>();
        player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");

        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = this.transform.position;
            mainCamera.transform.position = chaseScene.cameraLocation;
        }
    }
}