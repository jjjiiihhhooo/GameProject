using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_EyeSpawn : MonoBehaviour
{
    [SerializeField] GameObject white; // ÈòÀÚ
    [SerializeField] GameObject black; // °ËÀºÀÚ
    [SerializeField] GameObject eyelid; // ´«²¨Ç®

    float delay;
    float delay1;
    int dir;
    int blink;

    void Start()
    {
        delay = Random.Range(0.0f, 0.5f);
        delay1 = Random.Range(0.0f, 0.5f);
        dir = Random.Range(0, 2);
        blink = Random.Range(0, 2);

        eyelid.SetActive(false);
        white.SetActive(false);
        black.SetActive(false);

        StartCoroutine(EyeOpen());
    }

    IEnumerator EyeOpen()
    {
        yield return new WaitForSeconds(delay);
        eyelid.SetActive(true);
        white.SetActive(false);
        black.SetActive(false);

        yield return new WaitForSeconds(delay1);
        eyelid.SetActive(false);
        white.SetActive(true);
        black.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        while(true)
        {
            if(dir == 0)
            {
                while(black.transform.position.x < gameObject.transform.position.x + 0.1f)
                {
                    black.transform.Translate(0.2f * Time.deltaTime, 0, 0);
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);
                dir = 1;
            }
            else if(dir == 1)
            {
                while (black.transform.position.x > gameObject.transform.position.x - 0.1f)
                {
                    black.transform.Translate(-0.2f * Time.deltaTime, 0, 0);
                    yield return null;
                }
                yield return new WaitForSeconds(0.1f);
                dir = 0;
            }

            if(blink == 1)
            {
                eyelid.SetActive(true);
                white.SetActive(false);
                black.SetActive(false);

                yield return new WaitForSeconds(0.2f);

                eyelid.SetActive(false);
                white.SetActive(true);
                black.SetActive(true);
            }

            blink = Random.Range(0, 2);
        }
    }
}
