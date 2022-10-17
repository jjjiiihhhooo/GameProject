using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBook : MonoBehaviour
{
    [SerializeField] private GameObject puzzle_title;
    private bool isTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            isTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            isTrigger = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            puzzle_title.SetActive(isTrigger);
            isTrigger = false;
        }
    }
}
