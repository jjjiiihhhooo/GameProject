using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_JY_playground : MonoBehaviour
{
    public int count = 0;
    [SerializeField] GameObject ranbox;
    [SerializeField] GameObject giveNote;

    chap4_NoteBox NB;

    private void Start()
    {
        NB = giveNote.GetComponent<chap4_NoteBox>();
    }

    // Update is called once per frame
    void Update()
    {
        if(count == 6)
        {
            count = 0;
            ranbox.SetActive(false);
            giveNote.SetActive(true);
        }

        if (NB.noMore)
        {
            this.gameObject.SetActive(false);  
        }
    }
}
