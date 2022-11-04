using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Appear : MonoBehaviour
{
    private SpriteRenderer sprite;
    [SerializeField] private float UntilTime = 1;
    float time = 0;

    private void OnEnable()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(0, 0, 0, 0);
        StartCoroutine(appear());
    }

    private IEnumerator appear()
    {
        float fadeSpeed = UntilTime / 100;

        while (time < UntilTime)
        {
            time += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
            sprite.color = new Color(0, 0, 0, time);
        }
    }
}
