using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalTwo : MonoBehaviour
{
    [SerializeField] private BlackMob blackMob;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject mob;
    [SerializeField] private Transform mapTransform;

    [SerializeField] private GameObject chase;
    [SerializeField] private GameObject mainCamera;

    private void Awake()
    {
        Transform mobTransform = transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            chase.GetComponent<ChaseScene>().isChase = true;

            fade.SetActive(true);

            player.transform.position = mapTransform.position;
            mainCamera.transform.position = chase.GetComponent<ChaseScene>().cameraLocation;
            blackMob.transform.position = chase.GetComponent<ChaseScene>().mobLocation;

        }
    }
}
