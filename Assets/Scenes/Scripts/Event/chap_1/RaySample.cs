using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySample : MonoBehaviour
{
    BoxCollider2D box;
    [SerializeField] Animator ani;

    float x;
    float y;

    void Start()
    {
        box= GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        x = ani.GetFloat("DirX");
        y = ani.GetFloat("DirY");

        // ╩С
        if (x == 0 && y == 1)
        {
            box.size= new Vector2(0.2f, 5);
            box.offset = new Vector2(0.1f, 2.5f);
        }
        // го
        if (x == 0 && y == -1)
        {
            box.size = new Vector2(0.2f, 5);
            box.offset = new Vector2(0.1f, -2.5f);
        }
        // аб
        if (x == -1 && y == 0)
        {
            box.size = new Vector2(5, 0.2f);
            box.offset = new Vector2(-2.5f, 0.1f);
        }
        // ©Л
        if (x == 1 && y == 0)
        {
            box.size = new Vector2(5, 0.2f);
            box.offset = new Vector2(2.5f, 0.1f);
        }
    }
}
