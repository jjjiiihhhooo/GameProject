using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CameraManager cameraManager;

    [SerializeField] bool inEvent;
    [SerializeField] bool isCamera;

    [SerializeField] float DirX;
    [SerializeField] float DirY;

    void Start()
    {
        player.GetComponent<PlayerMove>().inEvent= inEvent;
        player.GetComponent<Animator>().SetFloat("DirX", DirX);
        player.GetComponent<Animator>().SetFloat("DirY", DirY);
        cameraManager.isCamera = isCamera;
    }
}
