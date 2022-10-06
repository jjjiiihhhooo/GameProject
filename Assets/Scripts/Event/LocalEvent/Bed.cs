using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform bed;
    [SerializeField] private Transform two;
    [SerializeField] private GameObject fadeout;
    private ChoiceEvent theChoiceEvent;
    private PlayerMove thePlayerMove;

    void Start()
    {
        theChoiceEvent = GetComponent<ChoiceEvent>();
        thePlayerMove = FindObjectOfType<PlayerMove>();
    }
    void Update()
    {
        if (theChoiceEvent.goEvent == true)
        {
            Debug.Log("true");
            theChoiceEvent.goEvent = false;
            player.transform.position = bed.position;
            fadeout.SetActive(true);

            StartCoroutine(WakeUp());
        }
    }

    IEnumerator WakeUp()
    {
        thePlayerMove.isMove = false;
        yield return new WaitForSeconds(1.0f);
        player.transform.position = two.position;
        thePlayerMove.isMove = true;
    }
}