using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class ChoiceEvent : MonoBehaviour
{
    public Choice choice;
    [SerializeField] private ChoiceManager theChoiceManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform mapTransform;
    [SerializeField] private int check = 0; //0�̸� �� �̵� �ϴ� NPC , 1�̸� ��ȣ�ۿ� ������(����, ħ��, ���)
    

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        theChoiceManager.isChoice = true;
        theChoiceManager.NpcChange(gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        theChoiceManager.isChoice = false;
        StopCoroutine(ACoroutine());
    }

    public void StartCoroutine()
    {
        StartCoroutine(ACoroutine());
    }

    public IEnumerator ACoroutine()
    {
        theChoiceManager.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoiceManager.choiceIng);
    }

    public void Action(int _result)
    {
        if(_result == 0 && check == 0)
        {
            player.transform.position = mapTransform.position;
        }
        else if(_result == 0 && check == 1)
        {
            gameObject.SetActive(false);
        }
    }
}
