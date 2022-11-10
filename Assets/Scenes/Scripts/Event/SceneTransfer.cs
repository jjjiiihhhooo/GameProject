using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    private SceneChanger sceneChanger;
    public string transferScene;

    void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            sceneChanger.currentScene = transferScene;
            SceneManager.LoadScene(transferScene);
        }
    }
}
