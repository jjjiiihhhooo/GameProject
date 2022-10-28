using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject menu_obj;
    [SerializeField] private GameObject option_obj;
    private bool isActive;
    private bool isOption;

    [SerializeField] private SaveManager saveManager;
    private void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SetActiveMenu();
        }
    }

    private void SetActiveMenu()
    {
        isActive = !isActive;
        menu_obj.SetActive(isActive);
        isOption = true;
        SetActiveOption();
    }

    private void SetActiveOption()
    {
        isOption = !isOption;
        option_obj.SetActive(isOption);
    }

    private void InputButton(int _count)
    {
        SetActiveMenu();

        if (_count == 0)
        {
            SetActiveOption();
        }
        else if(_count == 1)
        {
            SceneManager.LoadScene("Title");
        }
        else if(_count == 2)
        {
            saveManager.IsLoad();
        }
        else
        {
            Application.Quit();
        }
    }

    public void OptionExitButton()
    {
        SetActiveMenu();
    }

    public void ClickButton(int _count)
    {
        InputButton(_count);
    }


}
