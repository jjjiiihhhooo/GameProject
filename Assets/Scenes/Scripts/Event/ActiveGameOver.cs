using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOver_obj;


    public void SetActive(bool _bool)
    {
        gameOver_obj.SetActive(_bool);
    }

    
}
