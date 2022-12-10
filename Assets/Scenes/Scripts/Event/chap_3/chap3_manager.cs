using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap3_manager : MonoBehaviour
{
    [SerializeField] private GameObject blackSpace;
    [SerializeField] Transform mapTransform;
    [SerializeField] private GameObject player;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private GameObject fade;

    private void Awake()
    {
        //player = FindObjectOfType<PlayerMove>().gameObject;
        //cameraManager = FindObjectOfType<CameraManager>();
        player.transform.position = mapTransform.position;
        cameraManager.isCamera = false;
        cameraManager.Transform(mapTransform);
        if(fade != null)
        {
            fade.SetActive(true);
        }
    }

    public void SetActive()
    {
        blackSpace.SetActive(false);
        blackSpace.SetActive(true);
    }
}
