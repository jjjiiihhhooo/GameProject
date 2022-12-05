using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    SceneChanger sceneChanger;
    public string transferScene;
    [SerializeField] bool collOff;

    void Start()
    {
        sceneChanger = FindObjectOfType<SceneChanger>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collOff)
        {
            TransScene(transferScene);
        }
    }

    public void TransScene(string _scene)
    {
        sceneChanger.currentScene = _scene;
        SceneManager.LoadScene(_scene);
    }
}
