using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PlayerMove : MoveManager
{
    public static PlayerMove instance;

    [SerializeField] private float vertical;
    [SerializeField] private float horizontal;
    [SerializeField] private float runSpeed;
    public bool isMove = true;
    [SerializeField] private Bed bed;
    private ChatManager theChatManager;
    DialogueManager dialogueManager;
    private ChoiceManager theChoiceManager;
    public bool inEvent = false;
    private bool canMove;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        theChatManager = FindObjectOfType<ChatManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
        theChoiceManager = FindObjectOfType<ChoiceManager>();
        bed.BedTransform();

        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void BedActive()
    {
        bed.gameObject.SetActive(false);
    }
    private void Update()
    {
        canMove = !dialogueManager.onDialogue && !theChatManager.isChat2 && !theChoiceManager.isChoice2 && !inEvent ? true : false;
        //canMove = !dialogueManager.onDialogue && !theChatManager.isChat2 && !inEvent ? true : false; ///

        //Debug.Log("canMove: " + canMove);
        //Debug.Log(theChatManager.isChat2);
        //Debug.Log(theChoiceManager.isChoice2);
        //Debug.Log(chaseScene.isChase);
        //Debug.Log(inEvent);
        if (isMove && canMove) ///
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                isMove = false;
                StartCoroutine(MoveCoroutine());
            }
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
                transform.Translate(vector * speed * Time.deltaTime); // speed/1s
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
    public bool GetIsChat()
    {
        return theChatManager.isChat2;
    }
}