using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Puzzle puzzle;
    [SerializeField] private PuzzleMove puzzleMove;
    [SerializeField] GameObject Book;

    public Vector3 LoadedPos;
    private float startPosx;
    private float startPosY;
    private bool isBeingHeld = false;
    public bool isInLine;
    public float timeLinePosY;
    public int check = 0;

    AudioSource[] audioSource = new AudioSource[2];

    private void Start()
    {
        LoadedPos = this.transform.position;
        audioSource = Book.GetComponents<AudioSource>();
    }

    private void Update()
    {
        if(isBeingHeld)
        {
            Vector2 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            this.gameObject.transform.position = new Vector2(mousePos.x - startPosx, mousePos.y - startPosY);
        }
        if(isInLine)
        {
            LoadedPosSet();
        }
    }

    private void OnMouseDown()
    {
        if(Input.GetMouseButtonDown(0))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
            Vector3 mousePos;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            startPosx = mousePos.x - this.transform.position.x;
            startPosY = mousePos.y - this.transform.position.y;

            isBeingHeld = true;
        }
    }

    public void LoadedPosSet()
    {
        LoadedPos = this.transform.position;
    }

    private void OnMouseUp()
    {
        spriteRenderer.color = new Color(0.2f, 0.2f, 0.2f, 1f);
        isBeingHeld = false;

        if (isInLine)
        {
            audioSource[0].Play();
            puzzleMove.PuzzleMoving();
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.localPosition.x, timeLinePosY, 0);
        }
        else
        {
            audioSource[1].Play();
            this.gameObject.transform.position = this.LoadedPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("frame"))
        {
            if(check == puzzle.check)
            {

                isInLine = true;
                timeLinePosY = other.transform.position.y;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("frame"))
        {
            isInLine = false;
        }
    }
}
