using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image image;
    [SerializeField] private float fadeCheck = 1;

    private void OnEnable()
    {
        StartCoroutine(FadeINOUT());
        
    }

    private IEnumerator FadeINOUT()
    {

        while(fadeCheck > 0)
        {
            fadeCheck -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCheck);
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
