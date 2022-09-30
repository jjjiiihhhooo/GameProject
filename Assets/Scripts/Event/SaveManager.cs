using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private GameObject save_obj;
    private bool isSave = false;

    public void SaveActive()
    {
        isSave = !isSave;
        save_obj.SetActive(isSave);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SaveActive();
        }
    }
}
