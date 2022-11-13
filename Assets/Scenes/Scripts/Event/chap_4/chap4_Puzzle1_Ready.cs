using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_Puzzle1_Ready : MonoBehaviour
{
    /* 
     * ���� ��ü�� �����ϰ� ��ȭâ ����
     * ����, ���� �غ� �Ϸ�
     */

    [SerializeField] GameObject BlackShape;
    [SerializeField] GameObject JY_0;
    [SerializeField] GameObject JY_1;
    chap4_MapSpawner mapSpawner;
    DialogueBox dialogueBox;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        mapSpawner = GetComponent<chap4_MapSpawner>();
        StartCoroutine(AppearBlackShape(2.0f));
    }

    void Update()
    {
        
    }

    IEnumerator AppearBlackShape(float _waitTime)
    {
        yield return new WaitForSeconds(1.0f);
        BlackShape.SetActive(true);
        yield return new WaitForSeconds(_waitTime);

        dialogueBox.SetDialogue(); // ��...
        while(true)
        {
            if (dialogueBox.noMore && !dialogueBox.isLog)
            {
                JY_0.SetActive(false);
                JY_1.SetActive(true);
                mapSpawner.SetGetReady(true);
                break;
            }
            yield return null;
        }
    }
}