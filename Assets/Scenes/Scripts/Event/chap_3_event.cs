using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap_3_event : MonoBehaviour
{
    private DialogueBox[] dialogues;
    [SerializeField] private GameObject BlackSpace;
    [SerializeField] private CameraManager cameraManager;
    private bool isBool;


    private void Awake()
    {
        dialogues = GetComponents<DialogueBox>();
        cameraManager = FindObjectOfType<CameraManager>();
        isBool = true;
    }

    private void Update()
    {
        if (isBool && dialogues[dialogues.Length - 1].noMore)
        {
            cameraManager.Transform(BlackSpace.transform);
            BlackSpace.SetActive(true);
            isBool = false;
        }
    }


}
