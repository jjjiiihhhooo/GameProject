using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;

    public bool isOpen = true;

    public void StartBlink(string OpenOrClose, float time)
    {
        StartCoroutine(GetBlink(OpenOrClose, time));
    }

    IEnumerator GetBlink(string OpenOrClose, float time)
    {
        float timer = 0;

        if (OpenOrClose == "close")
        {
            isOpen = true;

            top.transform.position = this.transform.position;
            bottom.transform.position = this.transform.position;

            while (timer <= time)
            {
                top.transform.position = this.transform.position + new Vector3(0, ( -7 * timer * timer) / (time *time), 0);
                bottom.transform.position = this.transform.position + new Vector3(0, ( 7 * timer * timer ) / ( time * time ), 0);

                timer += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            isOpen = false;
        }

        if (OpenOrClose == "open")
        {
            isOpen = false;

            top.transform.position = this.transform.position + new Vector3(0, -7, 0);
            bottom.transform.position = this.transform.position + new Vector3(0, 7, 0);

            while (timer <= time)
            {
                top.transform.position = this.transform.position + new Vector3(0, -7, 0) + new Vector3(0, (7 * timer * timer) / (time * time), 0);
                bottom.transform.position = this.transform.position + new Vector3(0, 7, 0) + new Vector3(0, (-7 * timer * timer) / (time * time), 0);

                timer += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            isOpen = true;
        }
    }
}
