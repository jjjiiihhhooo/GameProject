using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aa : MonoBehaviour
{
    GameObject mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera.GetComponent<CameraManager>().isCamera = true;
    }
}
