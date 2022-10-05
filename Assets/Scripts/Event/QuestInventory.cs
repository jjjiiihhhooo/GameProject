using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] slot_obj;
    [SerializeField] private QuestManager theQuestManager;

    public void ItemChange(string _item, Sprite _image)
    {

        for(int i = 0; i < slot_obj.Length; i++)
        {
            if(slot_obj[i].GetComponent<Slot>().ex == null)
            {
                slot_obj[i].GetComponent<Slot>().ex = _image;
                slot_obj[i].GetComponent<Slot>().item_text.text = _item;
                slot_obj[i].GetComponent<Slot>().ChangeImage();
                return;
            }
        }

        
    }
}
