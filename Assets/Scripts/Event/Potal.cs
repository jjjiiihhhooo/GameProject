using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] private GameObject player; //�̵� �� ĳ����
    [SerializeField] private Transform mapTransform;
    [SerializeField] private GameObject fade;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player.transform.position = mapTransform.position;
            fade.SetActive(true);
        }
    }
}
