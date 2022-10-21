using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class QuestInventory : MonoBehaviour
{
    [SerializeField] private GameObject[] slot_obj;
    [SerializeField] private QuestManager theQuestManager;
    [SerializeField] private List<Slot> item;
    public List<Slot> itemList;

    private void Awake()
    {
    }

    public void ItemChange(string _item, Sprite _image)
    {

        for (int i = 0; i < slot_obj.Length; i++)
        {
            if (slot_obj[i].GetComponent<Slot>().ex == null)
            {
                slot_obj[i].GetComponent<Slot>().ex = _image;
                slot_obj[i].GetComponent<Slot>().item_text.text = _item;
                slot_obj[i].GetComponent<Slot>().ChangeImage();
                item[i] = slot_obj[i].GetComponent<Slot>();
                itemList[i] = item[i];
                return;
            }
        }
    }

    public List<Slot> SlotList()
    {
        return itemList;
    }

    public void SaveItem(List<Slot> _itemList)
    {
        itemList = _itemList;
    }

    public void LoadItem(List<Slot> _itemList)
    {
        for(int i = 0; i < _itemList.Count; i++)
        {
            itemList[0].ex = _itemList[i].ex;
            itemList[0].item_image = _itemList[i].item_image;
            itemList[0].item_text.text = _itemList[i].item_text.text;
        }

        for(int i = 0; i <_itemList.Count; i++)
        {
            slot_obj[i].GetComponent<Slot>().ex = itemList[0].ex;
            slot_obj[i].GetComponent<Slot>().item_image = itemList[0].item_image;
            slot_obj[i].GetComponent<Slot>().item_text.text = itemList[0].item_text.text;
        }
    }

}
