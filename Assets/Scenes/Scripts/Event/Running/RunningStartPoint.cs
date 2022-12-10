
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStartPoint : MonoBehaviour
{
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


        //sceneChanger = FindObjectOfType<SceneChanger>();
        //player = GameObject.Find("Player");

        if (sceneChanger == null)
            sceneChanger = FindObjectOfType<SceneChanger>();
        chaseScene = FindObjectOfType<ChaseScene>();
        if(player == null)
            player = GameObject.FindWithTag("Player");

        mainCamera = GameObject.FindWithTag("MainCamera");

        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = new Vector3(-1, -1.5f, 0);
            mainCamera.transform.position = this.transform.position + new Vector3(6, 2, -1);
            Debug.Log($"{player.transform.position.x}/{player.transform.position.y}/{player.transform.position.z}");
        }
    }
}