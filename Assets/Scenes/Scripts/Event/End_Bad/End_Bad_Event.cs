using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class End_Bad_Event : MonoBehaviour
{
    DialogueBox dialogueBox;
    [SerializeField] GameObject player;
    [SerializeField] GameObject blackWindow;
    Animator ani;

    bool isColl;

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        ani = GetComponent<Animator>();

        ani.SetFloat("DirX", -1);
        ani.SetFloat("DirY", 0);

        StartCoroutine(Event());
    }

    IEnumerator Event()
    {
        yield return new WaitForSeconds(3.0f);

        dialogueBox.SetDialogue();

        while (!dialogueBox.noMore)
        {
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        ani.SetBool("isWalk", true);
        yield return new WaitForSeconds(0.25f);

        while (true)
        {
            transform.Translate(Vector2.left * 4 * Time.deltaTime);
            yield return null;

            if (isColl)
                break;
        }

        blackWindow.SetActive(true);
        player.SetActive(false);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isColl = true;
        }
    }
}
