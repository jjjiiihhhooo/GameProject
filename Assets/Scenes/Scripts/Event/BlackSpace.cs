using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackSpace : MonoBehaviour
{

    private bool isTurn;
    [SerializeField] private int moveCount = 1;
    [SerializeField] private int playerMoveCount = 0;
    private PlayerMove player;
    private CameraManager cameraManager;
    private chap3_manager manager;
    [SerializeField] private GameObject puzzle_player;
    [SerializeField] private GameObject puzzle_jiyeon;
    [SerializeField] private GameObject[] mapNodes;
    [SerializeField] Transform blackSpaceTransform;
    


    private void OnEnable()
    {
        player = FindObjectOfType<PlayerMove>();
        cameraManager = FindObjectOfType<CameraManager>();
        manager = FindObjectOfType<chap3_manager>();
        player.isMove = false;
        moveCount = 1;
        cameraManager.Transform(blackSpaceTransform);

        puzzle_player.transform.position = mapNodes[0].transform.position;
        puzzle_jiyeon.transform.position = mapNodes[1].transform.position;

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (moveCount < mapNodes.Length)
        {
            yield return new WaitForSeconds(0.3f);
            puzzle_jiyeon.transform.position = mapNodes[moveCount].transform.position;
            moveCount++;
        }
    }

    private void ResetPosition()
    {
        puzzle_player.transform.position = mapNodes[0].transform.position;
        playerMoveCount = 0;

        manager.SetActive();
    }

    private void PlayerMove(int x, int y)
    {
        if(x != 0)
        {
            if(x > 0) //오른쪽
            {
                node checkNode = mapNodes[playerMoveCount].GetComponent<node>();
                if(checkNode.right == 1)
                {
                    if(checkNode.frontCheck == 2)
                    {
                        playerMoveCount++;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                    else if(checkNode.backCheck == 2)
                    {
                        playerMoveCount--;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                }
                else
                {
                    ResetPosition();
                }
            }
            else if(x < 1) // 왼쪽
            {
                node checkNode = mapNodes[playerMoveCount].GetComponent<node>();
                if (checkNode.left == 1)
                {
                    if (checkNode.frontCheck == 1)
                    {
                        playerMoveCount++;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                    else if (checkNode.backCheck == 1)
                    {
                        playerMoveCount--;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                }
                else
                {
                    ResetPosition();
                }
            }
        }
        else if(y != 0)
        {
            if(y > 0) // 아래
            {
                node checkNode = mapNodes[playerMoveCount].GetComponent<node>();
                if (checkNode.down == 1)
                {
                    if (checkNode.frontCheck == -2)
                    {
                        playerMoveCount++;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                    else if (checkNode.backCheck == -2)
                    {
                        playerMoveCount--;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                }
                else
                {
                    ResetPosition();
                }
            }
            else if(y < 1) // 위
            {
                node checkNode = mapNodes[playerMoveCount].GetComponent<node>();
                if (checkNode.up == 1)
                {
                    if (checkNode.frontCheck == -1)
                    {
                        playerMoveCount++;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                    else if (checkNode.backCheck == -1)
                    {
                        playerMoveCount--;
                        puzzle_player.transform.position = mapNodes[playerMoveCount].transform.position;
                    }
                }
                else
                {
                    ResetPosition();
                }
            }
        }
        
    }

    private void SetActiveNode()
    {
        for(int i = 0; i < mapNodes.Length; i++)
        {
            Renderer renderer = mapNodes[i].GetComponent<Renderer>();
            renderer.enabled = true;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerMove(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerMove(-1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerMove(0, -1);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerMove(0, 1);
        }

        if(playerMoveCount == 69 && !isTurn)
        {
            isTurn = true;
            SetActiveNode();
        }

    }


}
