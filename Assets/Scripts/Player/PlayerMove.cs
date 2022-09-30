using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MoveManager
{
    //»óÇÏÁÂ¿ì °ª
    [SerializeField] private float vertical;
    [SerializeField] private float horizontal;
    [SerializeField] private float runSpeed;
    private bool isMove = true;


    [SerializeField] private TestChat theTestChat;
    private ChatManager theChatManager;
    private ChoiceManager theChoiceManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        theChatManager = FindObjectOfType<ChatManager>();
        theChoiceManager = FindObjectOfType<ChoiceManager>();
    }

    private void Update()
    {
        if(isMove && !theChatManager.isChat2 && !theChoiceManager.isChoice2)
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                isMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        if(theChatManager.isChat)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                theTestChat.OpenChat();
                theChatManager.isChat = false;
            }
        }
    }

    private IEnumerator MoveCoroutine()
    {
        while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            
            vertical = Input.GetAxisRaw("Vertical");
            horizontal = Input.GetAxisRaw("Horizontal");

            Vector2 moveVector;

            moveVector.x = horizontal;
            moveVector.y = vertical;

            vector = moveVector;

            if (vector.x != 0)
                vector.y = 0;
            else if (vector.y != 0)
                vector.x = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walk", true);

            while (walkCount < walkCheck)
            {
                transform.Translate(vector * speed * Time.deltaTime);
                walkCount++;
                yield return new WaitForSeconds(0.01f);
            }
            walkCount = 0;
        }
        if(animator.GetBool("Walk"))
            animator.SetBool("Walk", false);
        isMove = true;
    }

}
