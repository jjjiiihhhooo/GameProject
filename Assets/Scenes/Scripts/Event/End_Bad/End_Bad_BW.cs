using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Bad_BW : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody2D rigid;
    [SerializeField] float time;
    float color_r;
    float color_g;
    float color_b;

    void Start()
    {
        spr= GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        StartCoroutine(GetBright());
    }

    IEnumerator GetBright()
    {
        float timer = 0;
        yield return new WaitForSeconds(2);

        while (timer < time)
        {
            color_r += Time.deltaTime/time;
            color_g += Time.deltaTime/time;
            color_b += Time.deltaTime/time;

            spr.color = new Color(color_r, color_g, color_b, 1);
            timer += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        rigid.velocity= Vector2.down;

        yield return new WaitForSeconds(12);

        rigid.velocity = Vector2.zero;
    }
}
