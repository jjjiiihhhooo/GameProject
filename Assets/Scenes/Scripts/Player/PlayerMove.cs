using System.Collections;
//using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerMove : MoveManager
{
    public static PlayerMove instance;

    [SerializeField] private float vertical;
    [SerializeField] private float horizontal;
    [SerializeField] private float runSpeed;
    [SerializeField] chap4_NoteManager NM;
    public bool isMove = true;
    [SerializeField] private Bed bed;
    [SerializeField] DialogueManager dialogueManager;
    public bool inEvent = false;
    private bool canMove;

    public bool indep = true;

    Rigidbody2D rigid;
    GameObject scanned;
    public Vector2 moveVector = new Vector2(-1, 0);

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        if(dialogueManager == null)
            dialogueManager = FindObjectOfType<DialogueManager>();

        if(bed != null)
            bed.BedTransform();

        //if (instance == null)
        //{
        //    DontDestroyOnLoad(this.gameObject);
        //    instance = this;
        //}
        //else
        //{
        //    Destroy(this.gameObject);
        //}
    }

    public void BedActive()
    {
        bed.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (NM)
            canMove = !dialogueManager.onDialogue && !NM.onDialogue && !inEvent ? true : false;
        else
            canMove = !dialogueManager.onDialogue && !inEvent ? true : false;

        if (canMove)
        {
            //if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            //{
            //isMove = false;
            //StartCoroutine(MoveCoroutine());

            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Walk", true);
                vertical = Input.GetAxisRaw("Vertical");
                horizontal = Input.GetAxisRaw("Horizontal");
                moveVector.x = horizontal;
                moveVector.y = vertical;
                vector = moveVector; // (1, 0)
                if (vector.x != 0)
                    vector.y = 0;
                else if (vector.y != 0)
                    vector.x = 0;
                animator.SetFloat("DirX", vector.x);
                animator.SetFloat("DirY", vector.y);
                rigid.velocity = vector * speed;
            }
            else
            {
                animator.SetBool("Walk", false);
                rigid.velocity = Vector2.zero;
            }
        }
        else
        {
            if (indep)
            {
                rigid.velocity = Vector2.zero;
                animator.SetBool("Walk", false);
            }
        }

        // ray
        Debug.DrawRay(rigid.position, vector * 1.2f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, vector * 1.2f, LayerMask.GetMask("Object"));

        if(rayHit.collider != null)
        {

        }
    }
    private IEnumerator MoveCoroutine()
    {
        while ((Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) && canMove) ///
        {
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");
            Vector2 moveVector;
            moveVector.x = horizontal;
            moveVector.y = vertical;
            vector = moveVector; // (1, 0)
            if (vector.x != 0)
                vector.y = 0;
            else if (vector.y != 0)
                vector.x = 0;
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walk", true);

            while (walkCount < walkCheck)
            {
                //transform.Translate(vector * speed * Time.deltaTime); // speed/1s
                rigid.velocity = vector * speed;
                walkCount++;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            walkCount = 0;
        }
        if (animator.GetBool("Walk"))
            animator.SetBool("Walk", false);
        isMove = true;
    }
    public bool GetCanMove()
    {
        return canMove;
    }
}