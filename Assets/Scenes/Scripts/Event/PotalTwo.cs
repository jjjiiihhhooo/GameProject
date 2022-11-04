using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PotalTwo : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mob;
    [SerializeField] private GameObject mainCamera;
    private SceneChanger sceneChanger;
    public string transferScene;

    private void Start()
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