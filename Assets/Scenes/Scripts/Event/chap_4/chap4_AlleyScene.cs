using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_AlleyScene : MonoBehaviour
{
    // 플레이어 이동 위치
    [SerializeField] Transform playground;
    [SerializeField] Transform from;
    [SerializeField] chap4_ToB to;
    [SerializeField] GameObject intro;

    [SerializeField] GameObject player;
    [SerializeField] GameObject mainCamera;
    Animator pAni;

    float speed = 8.0f;

    void Start()
    {
        pAni = player.GetComponent<Animator>();
        player.transform.position = from.position;
        mainCamera.transform.position = this.gameObject.transform.position + new Vector3(0,0,-1);
        player.GetComponent<PlayerMove>().inEvent= true;
        StartCoroutine(StartSecen());
    }

    IEnumerator StartSecen()
    {
        while(!to.Finish)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.left * speed;
            pAni.SetFloat("DirY", 0);
            pAni.SetFloat("DirX", -1);
            pAni.SetBool("Walk", true);
            yield return null;
        }
        pAni.SetFloat("DirX", 0);
        pAni.SetFloat("DirY", -1);
        pAni.SetBool("Walk", false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = playground.position;
        intro.SetActive(true);
        mainCamera.GetComponent<CameraManager>().Bool();
        mainCamera.GetComponent<CameraManager>().target_obj = player;
        StopAllCoroutines();
    }
}
