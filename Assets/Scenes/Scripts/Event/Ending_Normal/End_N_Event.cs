using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End_N_Event : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject creature;
    [SerializeField] GameObject end;
    Animator ani;

    void Start()
    {
        ani = player.GetComponent<Animator>();
        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));

        StartCoroutine(Event());
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(3.0f);

        player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0.0f));
        player.transform.position = gameObject.transform.position;

        ani.SetFloat("DirX", 1);
        ani.SetFloat("DirY", 0);

        yield return new WaitForSeconds(1.0f);

        player.GetComponent<PlayerMove>().indep = false;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.right * 3;
        ani.SetBool("Walk", true);

        while (!end.activeSelf)
        {
            yield return null;
        }

        player.SetActive(false);
    }
}