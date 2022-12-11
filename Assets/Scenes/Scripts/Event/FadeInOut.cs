using System.Collections;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    SpriteRenderer spr;
    float color_a;
    public bool isOn;

    void Start()
    {
        spr= GetComponent<SpriteRenderer>();
    }
    
    public void StartFade(string _InOrOut, string _BlackOrWhite, float time)
    {
        StartCoroutine(Fade(_InOrOut, _BlackOrWhite, time));
    }

    IEnumerator Fade(string _InOrOut, string _BlackOrWhite, float time)
    {
        float timer = 0;

        if(_BlackOrWhite == "black")
            spr.color= Color.black;
        else if(_BlackOrWhite == "white")
            spr.color = Color.white;

        if (_InOrOut == "in")
        {
            isOn = true;

            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 1);
            while (timer < time)
            {
                color_a -= Time.deltaTime / time;

                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, color_a);
                timer += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            isOn= false;
        }
        else if(_InOrOut == "out")
        {
            isOn = false;

            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 0);
            while (timer < time)
            {
                color_a += Time.deltaTime / time;

                spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, color_a);
                timer += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            isOn = true;
        }
    }
}