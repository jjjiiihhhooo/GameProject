using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap3_manager : MonoBehaviour
{
    [SerializeField] private GameObject blackSpace;

    public void SetActive()
    {
        blackSpace.SetActive(false);
        blackSpace.SetActive(true);
    }
}
