using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Slot : MonoBehaviour
{
    public SlotData slotData;

    public Image item_image;
    public Text item_text;
    public Sprite ex;

    public void ChangeImage()
    {
        item_image.sprite = ex;
    }

    private void Update()
    {
        //if(item_image != null)
        //{
        //    //this.slotData.ex = this.item_image.sprite;
        //    this.slotData.item_text = this.item_text.text;
        //}
    }

}

[Serializable]
public class SlotData
{
    public string item_text;
    //public Image item_image;
    //public Text item_text;
    //public Sprite ex;
}
