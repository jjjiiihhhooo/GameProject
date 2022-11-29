using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    GameObject player;
    SceneChanger sceneChanger;
    [SerializeField] private CameraManager cameraManager;
    public string startPoint;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        sceneChanger = FindObjectOfType<SceneChanger>();
        cameraManager = FindObjectOfType<CameraManager>();
        cameraManager.Transform(this.gameObject.transform);
        player.GetComponent<PlayerMove>().isMove = true;
        if (startPoint == sceneChanger.currentScene)
        {
            player.transform.position = this.gameObject.transform.position;
        }
    }
}
