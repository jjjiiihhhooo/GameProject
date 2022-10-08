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
            theChoiceEvent.goEvent = false;
            player.transform.position = bed.position;
            thePlayerMove.SetIsMove(false);
            fadeout.SetActive(true);

            StartCoroutine(WakeUp());
        }
    }

    IEnumerator WakeUp()
    {
        yield return new WaitUntil(() => fadeout.activeSelf == false);
        player.transform.position = two.position;
        thePlayerMove.SetIsMove(true);
    }
}