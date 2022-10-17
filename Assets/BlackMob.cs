using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMob : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 sizeSpeed;
    public bool isStart;
    public bool isGo;
    private void Awake()
    {
        Invoke("IsStart", 1.5f);
        sizeSpeed = new Vector3(0.1f, 0.15f, 0);
    }

    private void IsStart()
    {
        isStart = true;
    }

    private void Update()
    {
        if(isStart && !isGo)
        {
            if(transform.position.x > -8f)
            {
                isGo = true;
            }
            else
            {
                this.transform.position = new Vector3(transform.position.x + 3 * Time.deltaTime, transform.position.y, transform.position.z);
            }
        }

        if (isGo)
        {
            this.transform.position = new Vector3(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y, transform.position.z);
            this.transform.localScale += sizeSpeed * Time.deltaTime;
        }
    }
}
