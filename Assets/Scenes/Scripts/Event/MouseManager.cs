using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{
    private bool keyInput;
    private int result;
    private int count;

    [SerializeField] private Title_ButtonManager buttonManager;
    public GameObject[] Text;



    private void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (result > 0)
                    result--;
                else
                    result = count;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (result < count)
                    result++;
                else
                    result = 0;
                Selection();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                keyInput = false;
                if (result == 0)
                {
                    buttonManager.MapStart(0);
                }
                else if(result == 1)
                {
                    buttonManager.MapStart(0);
                }
                else if(result == 2)
                {
                    buttonManager.Option();
                }

            }
        }
    }

    public void Selection()
    {
        Color color = Text[0].GetComponent<Text>().color;
        color.a = 0.75f;
        for (int i = 0; i <= count; i++)
        {
            Text[i].GetComponent<Text>().color = color;
        }
        color.a = 1f;
        Text[result].GetComponent<Text>().color = color;
    }
}
