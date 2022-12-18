using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_Bad_BW : MonoBehaviour
{
    SpriteRenderer spr;
    Rigidbody2D rigid;
    [SerializeField] float time;
    [SerializeField] GameObject DB;
    [SerializeField] GameObject hang;
    Rigidbody2D rigidDB;
    float color_r;
    float color_g;
    float color_b;

    [SerializeField] FadeInOut fade;
    AudioSource audioSource;
    SceneTransfer sceneTransfer;

    void Start()
    {
        spr= GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        rigidDB = DB.GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
        sceneTransfer = GetComponent<SceneTransfer>();

        audioSource.Play();
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
        rigidDB.velocity = Vector2.down;

        yield return new WaitForSeconds(3);

        fade.StartFade("out", "black", 5);

        yield return new WaitForSeconds(8);
        rigid.velocity = Vector2.zero;
        rigidDB.velocity = Vector2.zero;
        hang.SetActive(true);

        fade.StartFade("in", "black", 1);

        yield return new WaitForSeconds(5.0f);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                sceneTransfer.TransScene("Title");
                break;
            }
            yield return null;
        }
    }
}
