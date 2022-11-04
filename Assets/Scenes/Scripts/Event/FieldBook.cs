using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldBook : MonoBehaviour
{
    [SerializeField] private GameObject puzzle_title;
    [SerializeField] private ShowEmotion showEmotion;
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
            if(isTrigger)
            {
                if (showEmotion != null)
                    showEmotion.ShowEmotions(1); //물음표 모양
                if(puzzle_title != null)
                    puzzle_title.SetActive(true);
            }
                
            
            isTrigger = false;
        }
    }
}
