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

        //먼저 검은 형상 등장
        for (int i = 0; i < darkShape.Length; i++)
            darkShape[i].SetActive(true);
        // 검은 형상 등장 후
        yield return new WaitForSeconds(1.1f);

        //선택창이 담긴 오브젝트 활성화
        Qbox.SetActive(true);
    }
}
