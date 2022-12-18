using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap3_isWithJY : MonoBehaviour
{
    SceneChanger sceneChanger;
    DialogueBox[] dialogueBoxes;

    void Start()
    {
        sceneChanger= FindObjectOfType<SceneChanger>();
        dialogueBoxes= GetComponents<DialogueBox>();
    }

    void Update()
    {
        //if(dialogueBoxes[2].isStarted) Debug.Log(dialogueBoxes[2].dialogueManager.result[3]);
        if (dialogueBoxes[2].noMore && dialogueBoxes[2].dialogueManager.result[3] == 0)
            sceneChanger.chap3JY = true;
    }
}
