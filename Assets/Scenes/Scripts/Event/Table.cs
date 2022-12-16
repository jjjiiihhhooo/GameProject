using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    public bool isTable;
    public int pieceCount = 0;
    [SerializeField] private GameObject player;
    public GameObject sketchbook;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private GameObject trigger;
    //[SerializeField] private GameObject bed;
    [SerializeField] private GameObject npc2_dialogue;
    [SerializeField] private GameObject npc_dialogue;
    //[SerializeField] private TestChat npc2_testChat;
    Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        //CanMove();
    }

    void Update()
    {
        if (rigid.velocity.x > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x - 0.1f, rigid.velocity.y);
            if (rigid.velocity.x > 0.1f && rigid.velocity.x < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.x < 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x + 0.1f, rigid.velocity.y);
            if (rigid.velocity.x > 0.1f && rigid.velocity.x < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.x == 0) rigid.velocity = Vector2.zero;

        if (rigid.velocity.y > 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y - 0.1f);
            if (rigid.velocity.y > 0.1f && rigid.velocity.y < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.y < 0)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + 0.1f);
            if (rigid.velocity.y > 0.1f && rigid.velocity.y < -0.1f) rigid.velocity = Vector2.zero;
        }
        else if (rigid.velocity.y == 0) rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trigger")
        {
            isTable = true;
            box.isTrigger = true;
            npc_dialogue.SetActive(false);
            npc2_dialogue.SetActive(true);
            //npc2_dialogue.GetComponent<DialogueBox>().SetDialogue();
            player.GetComponent<PlayerMove>().BedActive();
            //bed.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Trigger")
        {
            isTable = false;
        }
    }

    public void AcitveBook()
    {
        sketchbook.SetActive(true);
    }

    public void CanMove()
    {
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
