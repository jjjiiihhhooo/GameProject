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
    public bool isMove = true;
    [SerializeField] private Bed bed;
    private ChatManager theChatManager;
    private ChoiceManager theChoiceManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        theChatManager = FindObjectOfType<ChatManager>();
        theChoiceManager = FindObjectOfType<ChoiceManager>();
        bed.BedTransform();
    }

    public void BedActive()
    {
        bed.gameObject.SetActive(false);
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
        if(animator.GetBool("Walk"))
            animator.SetBool("Walk", false);
        isMove = true;
    }

    public void SetIsMove(bool _bool)
    {
        isMove = _bool;
    }
}
