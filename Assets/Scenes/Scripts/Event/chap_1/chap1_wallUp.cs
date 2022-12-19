using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap1_wallUp : MonoBehaviour
{
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "chap1_table")
        {
            rigid.mass = 1;
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void Update()
    {
        if (rigid.velocity.x > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x - 0.1f, rigid.velocity.y);
            if (rigid.velocity.x > 0.1f && rigid.velocity.x < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.x < 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x + 0.1f, rigid.velocity.y);
            if (rigid.velocity.x > 0.1f && rigid.velocity.x < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.x == 0) rigid.velocity = Vector2.zero;

        if (rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y - 0.1f);
            if (rigid.velocity.y > 0.1f && rigid.velocity.y < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.y < 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + 0.1f);
            if (rigid.velocity.y > 0.1f && rigid.velocity.y < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.y == 0) rigid.velocity = Vector2.zero;
    }
}
