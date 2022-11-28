using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueBoxBox;

public class chap4_BlackMob : MonoBehaviour
{
    [SerializeField] float goalSize;
    [SerializeField] float time;
    [SerializeField] float time1;
    [SerializeField] float time2;

    [SerializeField] GameObject blackMob;
    [SerializeField] Transform loc0;
    [SerializeField] Transform loc1;
    [SerializeField] Transform loc2;
    [SerializeField] Animator ani;

    int posX = -1;
    int posY = -1;
    Vector3 sizeSpeed;

    int phase = 0;
    bool single = true;
    //bool single1 = true;

    float basicSpeed = 0;
    float speed;
    float nightSpeed;
    float currentSpeed;

    void Start()
    {
        basicSpeed = (goalSize - blackMob.transform.localScale.x) / time;
        sizeSpeed = new Vector3(basicSpeed / 1, basicSpeed / 1, 0.0f);

        speed = (Vector2.Distance(loc0.position, loc1.position)) / time;
        currentSpeed = speed;
        nightSpeed = 1.5f * speed;
    }

    void Update()
    {
        if (posX == 12 && posY == 6)
        {
            SetPosN();
            blackMob.SetActive(true);
            phase++;
        }
        else if (phase == 1 && single)
        {
            single = false;
            StartCoroutine(BlackMobGrow(goalSize));
            StartCoroutine(BlackMobMove(loc1, speed));
        }
        else if (phase == 2 && single)
        {
            single = false;
            speed = (Vector2.Distance(loc1.position, loc2.position)) / time1;
            StartCoroutine(BlackMobMove(loc2, speed));
        }
        else if(phase == 3 && single)
        {
            phase++;
            ani.SetFloat("DirX", -1);
        }
        if(posX == 4 && posY == 4 && single)
        {
            single = false;
            StartCoroutine(BlackMobGrow(goalSize));
        }
    }

    IEnumerator BlackMobGrow(float _goal)
    {
        while (true)
        {
            blackMob.transform.localScale += sizeSpeed * Time.deltaTime;
            yield return null;
            if (blackMob.transform.localScale.x > _goal)
                break;
        }
        goalSize = 20.0f;
    }

    IEnumerator BlackMobMove(Transform _to, float _speed)
    {
        single = false;
        while (true)
        {
            blackMob.transform.position = Vector2.MoveTowards(blackMob.transform.position, _to.position, _speed * Time.deltaTime);
            yield return null;

            if (Vector2.Distance(_to.position, blackMob.transform.position) < 0.001f)
            {
                phase++;
                single = true;
                break;
            }
        }
    }


    public void SetPos(int _x, int _y)
    {
        posX = _x;
        posY = _y;
    }

    void SetPosN()
    {
        posX = -1;
        posY = -1;
    }
}
