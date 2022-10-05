using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image item_image;
    public Text item_text;
    public Sprite ex;

    public void ChangeImage()
    {
        item_image.sprite = ex;
    }

}
