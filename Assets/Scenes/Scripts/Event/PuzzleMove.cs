using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class PuzzleMove : MonoBehaviour
{
    [SerializeField] private GameObject[] books;
    [SerializeField] private GameObject[] piece;
    [SerializeField] private GameObject fade;
    [SerializeField] private GameObject fade2;
    [SerializeField] private GameObject npc_idle_obj;
    [SerializeField] private GameObject npc_idle_obj2;
    [SerializeField] private Table table;
    [SerializeField] private QuestInventory inventory;
    //[SerializeField] TestChat testChat;
    private int count;

    public void PuzzleMoving()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {

        if (count < books.Length)
        {
            books[count].SetActive(false);
            count++;
            if(count < books.Length)
                books[count].SetActive(true);
            else
            {
                table.rigid.mass = 1;
                table.sketchbook.SetActive(false);

                //testChat.ImageUpdate();
                //npc_idle_obj.SetActive(false);
                //npc_idle_obj2.SetActive(true);
                fade.SetActive(true);
                fade2.SetActive(true);
                inventory.ItemReset();
            }
        }
        

        for (int i = 0; i< 16; i++)
        {
            transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }

        for(int i = 0; i < piece.Length; i++)
        {
            piece[i].GetComponent<PuzzlePiece>().LoadedPosSet();
        }

    }
}
