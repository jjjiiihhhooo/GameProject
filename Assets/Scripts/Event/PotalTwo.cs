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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            blackMob.isStart = false;
            fade.SetActive(true);
            player.transform.position = mapTransform.position;
            mob.SetActive(false);

            chase.GetComponent<ChaseScene>().isChase = true;
        }
    }
}
