using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ghost : MonoBehaviour
{
    [SerializeField] private bool isTouch;
    [SerializeField] private bool isTouch2;
    [SerializeField] private Mob mob;
    [SerializeField] private TestChat testChat;


    bool getCount = false; ///


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            isTouch = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isTouch = false;
            if (mob.count > 3)
                testChat.isTarget = false;

            if (getCount) ///
            {
                mob.Count();
                getCount = false;
            }
        }
    }


    private void Update()
    {
        if(isTouch2 && isTouch && Input.GetKeyDown(KeyCode.Space))
        {
            isTouch = false;
            isTouch2 = false;
            // mob.Count();
            getCount = true; ///
        }
    }
}
