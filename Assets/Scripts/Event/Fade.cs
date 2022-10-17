using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image image;
    [SerializeField] private float fadeCheck = 1;
    [SerializeField] private float fadeSecond;
    [SerializeField] private float fadeCount;
    [SerializeField] private float color_r;
    [SerializeField] private float color_g;
    [SerializeField] private float color_b;

    private void OnEnable()
    {
        StartCoroutine(FadeINOUT());
        
    }

    private IEnumerator FadeINOUT()
    {

        if(fadeCount > 0)
        {
            while (fadeCheck > 0)
            {
                fadeCheck -= 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(color_r, color_g, color_b, fadeCheck);
            }
            while (fadeCheck < 1)
            {
                fadeCheck += 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(color_r, color_g, color_b, fadeCheck);
            }
            while(fadeCheck > 0)
            {
                fadeCheck -= 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(color_r, color_g, color_b, fadeCheck);
            }
        }
        else
        {
            while(fadeCheck > 0)
            {
                fadeCheck -= 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(color_r, color_g, color_b, fadeCheck);
            }
        }
            
        
        FadeReset();
    }

    private void FadeReset()
    {
        if (fadeCheck <= 0)
        {
            StopCoroutine(FadeINOUT());
            fadeCheck = 1;
            gameObject.SetActive(false);
        }
    }

}
