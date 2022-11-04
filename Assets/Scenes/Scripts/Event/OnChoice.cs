using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnChoice : MonoBehaviour
{
    [SerializeField] private ChoiceEvent theChoiceEvent;

    void OnEnable()
    {
        theChoiceEvent = GetComponent<ChoiceEvent>();
        theChoiceEvent.StartCoroutine();
    }
}
