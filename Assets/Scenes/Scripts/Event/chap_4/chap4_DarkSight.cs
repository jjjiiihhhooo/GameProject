using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_DarkSight : MonoBehaviour
{
    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if( timer >= 0.5f)
        {
            timer= 0;
            this.gameObject.SetActive(false);
        }
    }
}
