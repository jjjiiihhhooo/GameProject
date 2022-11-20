using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aaa : MonoBehaviour
{
    [SerializeField] GameObject puzzle;

    void Start()
    {
        puzzle.SetActive(true);
    }
}
