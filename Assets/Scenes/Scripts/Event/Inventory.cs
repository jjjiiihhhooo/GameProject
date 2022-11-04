using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject etcInventory;
    [SerializeField] private GameObject questInventory;
    public bool isInventory = false;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            isInventory = !isInventory;
            inventory.SetActive(isInventory);
        }

    }

    public void ETCOpen()
    {
        questInventory.SetActive(false);
        etcInventory.SetActive(true);
    }
    
    public void QuestOpen()
    {
        etcInventory.SetActive(false);
        questInventory.SetActive(true);
       
    }

}
