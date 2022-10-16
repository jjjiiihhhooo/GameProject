using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField] private bool isTrigger = true;
    [SerializeField] private float speed = 1;
    public int count;
    [SerializeField] private bool isStart;
    [SerializeField] private TestChat testChat;

    public void Bool()
    {
        isStart = true;
        isTrigger = false;
        testChat.isTrigger = true;
    }

    public void Count()
    {
        count++;
    }

    private void Update()
    {
        if (!isStart && count > 3)
            Bool();

        if (!isTrigger)
            Invoke("Moving", 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "MobPar")
        {
            isTrigger = true;
            other.GetComponent<MobPar>().Chat();
        }
            
    }

    private void Moving()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
    }
        
}
