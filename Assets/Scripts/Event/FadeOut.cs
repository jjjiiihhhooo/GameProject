using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image image;
    float time = 0;
    [SerializeField] private float UntilTime = 1;


    private void OnEnable()
    {
        StartCoroutine(FadeOUt());
    }

    private IEnumerator FadeOUt()
    {
        float fadeSpeed = UntilTime / 100; // = 0.01

        while (time < UntilTime)
        {
            time += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, time);
        }
        FadeReset();
    }

    private void FadeReset()
    {
        if (time > 0)
        {
            StopCoroutine(FadeOUt());
            time = 0;
            gameObject.SetActive(false);
        }
    }

}