using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap3_manager : MonoBehaviour
{
    [SerializeField] private GameObject blackSpace;
    [SerializeField] private GameObject fade;

    private void Awake()
    {
        fade = FindObjectOfType<Fade>().gameObject;
        fade.SetActive(true);
    }

    public void SetActive()
    {
        blackSpace.SetActive(false);
        blackSpace.SetActive(true);
    }
}
