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
                image.color = new Color(0, 0, 0, fadeCheck);
            }
            while (fadeCheck < 1)
            {
                fadeCheck += 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(0, 0, 0, fadeCheck);
            }
            while(fadeCheck > 0)
            {
                fadeCheck -= 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(0, 0, 0, fadeCheck);
            }
        }
        else
        {
            while(fadeCheck > 0)
            {
                fadeCheck -= 0.01f;
                yield return new WaitForSeconds(fadeSecond);
                image.color = new Color(0, 0, 0, fadeCheck);
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
