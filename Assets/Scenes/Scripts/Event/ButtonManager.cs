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
    [SerializeField] private ActiveGameOver gameOver;

    void Update()
    {
        if(menu_obj != null && Input.GetKeyDown(KeyCode.Escape))
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
        if (option_obj != null)
        {
            isOption = !isOption;
            option_obj.SetActive(isOption);
        }
    }

    private void InputButton(int _count)
    {
        //if (menu_obj != null)
        //    SetActiveMenu();

        if (_count == 0)
        {
            SetActiveOption();
        }
        else if (_count == 1)
        {
            SceneManager.LoadScene("Title");
        }
        else if (_count == 2)
        {
            saveManager.IsLoad();
        }
        else if (_count == 3)
        {
            gameOver.SetActive(false);
            ReloadScene();
        }
        else if (_count == 4)
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

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
