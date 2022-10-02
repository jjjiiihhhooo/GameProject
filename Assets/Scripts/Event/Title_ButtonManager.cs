using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject option_obj;
    private bool isOption;

    public void Option()
    {
        isOption = !isOption;
        option_obj.SetActive(isOption);
    }

    public void MapStart(int count)
    {
        if(count == 0)
        {
            SceneManager.LoadScene(0);
        }
        else if(count == 1)
        {
            SceneManager.LoadScene(1);
        }
    }
}
