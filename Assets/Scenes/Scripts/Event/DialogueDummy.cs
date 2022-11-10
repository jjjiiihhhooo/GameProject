using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDummy : MonoBehaviour
{
    [SerializeField] int before;
    [SerializeField] int after;
    [TextArea(1, 2)]
    [SerializeField] string readMe;
}
