using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_3D : MonoBehaviour
{
    [SerializeField] Transform player;
    SpriteRenderer spr;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.position.y - 0.925f >= transform.position.y)
            spr.sortingOrder = 4;
        else
            spr.sortingOrder = 2;
    }
}
