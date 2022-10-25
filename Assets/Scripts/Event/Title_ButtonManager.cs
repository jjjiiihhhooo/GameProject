using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject option_obj;
    [SerializeField] private SaveManager saveManager;
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
            SceneManager.LoadScene("Main"); //새 게임
        }
        else if(count == 1)
        {
            SceneManager.LoadScene(1); //이어 하기
        }
    }
}
