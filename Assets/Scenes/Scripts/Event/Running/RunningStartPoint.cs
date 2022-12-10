
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStartPoint : MonoBehaviour
{
    [SerializeField] ChaseScene chaseScene;
    public string startPoint;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mainCamera;

    void Start()
    {
        if(fade == null)
        fade = GameObject.FindWithTag("Canvas").transform.Find("Fade").gameObject;
        fade.SetActive(true);

        if (sceneChanger)
            sceneChanger = FindObjectOfType<SceneChanger>();
        chaseScene = FindObjectOfType<ChaseScene>();
        if(player == null)
            player = GameObject.FindWithTag("Player");
        mainCamera = GameObject.FindWithTag("MainCamera");

        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = this.transform.position;
            mainCamera.transform.position = chaseScene.cameraLocation;
        }
    }
}