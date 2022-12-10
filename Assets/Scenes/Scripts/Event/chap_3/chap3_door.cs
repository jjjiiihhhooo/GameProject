using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chap3_door : MonoBehaviour
{
    public bool isDoor = false;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "chap3_player")
        {
            if (isDoor)
            {
                isDoor = false;
                SceneManager.LoadScene("Game_4");
            }
        }
    }
}
