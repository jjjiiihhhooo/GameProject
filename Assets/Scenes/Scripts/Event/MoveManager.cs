using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    protected int walkCount;
    public int walkCheck;
    public float speed = 10;

    public Vector2 vector = new Vector2(0,0);

    //필요한 컴포넌트
    public Animator animator;


}
