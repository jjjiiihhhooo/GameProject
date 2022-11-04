using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rides : MonoBehaviour
{
    [SerializeField] private GameObject[] darkShape;
    [SerializeField] private GameObject Qbox;
    private ChoiceEvent theChoiceEvent;
    bool isOn = false;

    private void Start()
    {
        theChoiceEvent = FindObjectOfType<ChoiceEvent>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            isOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            isOn = false;
    }

    void Update()
    {
        if (isOn && Input.GetKeyDown(KeyCode.Space))
        {
            isOn = false;
            StartCoroutine(RidesInteract());
        }
    }

    private IEnumerator RidesInteract()
    {
        theChoiceEvent.SetIsChoice2(true);

        //���� ���� ���� ����
        for (int i = 0; i < darkShape.Length; i++)
            darkShape[i].SetActive(true);
        // ���� ���� ���� ��
        yield return new WaitForSeconds(1.1f);

        //����â�� ��� ������Ʈ Ȱ��ȭ
        Qbox.SetActive(true);
    }
}
