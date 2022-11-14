using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSpace : MonoBehaviour
{
    private GameObject player;
    private CameraManager cameraManager;
    [SerializeField] Transform blackSpaceTransform;

    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMove>().gameObject;
        cameraManager = FindObjectOfType<CameraManager>();
        player.transform.position = blackSpaceTransform.position;
        cameraManager.Bool();
    }




}
