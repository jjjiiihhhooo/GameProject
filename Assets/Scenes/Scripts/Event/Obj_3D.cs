using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj_3D : MonoBehaviour
{
    [SerializeField] Transform player;
    SpriteRenderer spr;

    [SerializeField] int before;
    [SerializeField] int after;
    [SerializeField] float dis;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.position.y - 0.925f + dis >= transform.position.y)
            spr.sortingOrder = after;
        else
            spr.sortingOrder = before;
    }
}
