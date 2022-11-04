using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowEmotion : MonoBehaviour
{
    [SerializeField] private GameObject[] emotions;

    public void ShowEmotions(int _index)
    {
        StartCoroutine(Show(_index));
        
    }

    private IEnumerator Show(int _index)
    {
        emotions[_index].SetActive(true);
        yield return new WaitForSeconds(1f);
        emotions[_index].SetActive(false);
        StopCoroutine(Show(_index));
    }
}
