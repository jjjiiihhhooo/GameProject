using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private chap3_door door;
    [SerializeField] private GameObject puzzle_player;
    [SerializeField] private GameObject puzzle_jiyeon;
    [SerializeField] private GameObject puzzle_jiyeon_image;
    [SerializeField] private GameObject puzzle_player_image;
    [SerializeField] private GameObject puzzle_mob;
    [SerializeField] private GameObject[] mapNodes;
    [SerializeField] private Transform blackSpaceTransform;
    [SerializeField] private Sprite[] palyerImage;
    [SerializeField] private Sprite[] jiyeonImage;
    private SpriteRenderer puzzlePlayer_renderer;
    private SpriteRenderer puzzleJiyeon_renderer;


    private void OnEnable()
    {
        puzzlePlayer_renderer = puzzle_player_image.GetComponent<SpriteRenderer>();
        puzzleJiyeon_renderer = puzzle_jiyeon_image.GetComponent<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>();
        cameraManager = FindObjectOfType<CameraManager>();
        manager = FindObjectOfType<chap3_manager>();
        player.isMove = false;
        isTurn = false;
        moveCount = 1;
        cameraManager.Transform(blackSpaceTransform);

        puzzle_mob.transform.position = mapNodes[70].transform.position;
        Renderer renderer1 = puzzle_mob.GetComponentInChildren<Renderer>();
        renderer1.enabled = false;
        puzzle_player.transform.position = mapNodes[0].transform.position;
        puzzle_jiyeon.transform.position = mapNodes[1].transform.position;

        for (int i = 1; i < mapNodes.Length - 2; i++)
        {
            Renderer renderer = mapNodes[i].GetComponent<Renderer>();
            renderer.enabled = false;
        }

        StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        while (moveCount < mapNodes.Length)
        {
            yield return new WaitForSeconds(0.4f);
            puzzle_jiyeon.transform.position = mapNodes[moveCount].transform.position;
            int temp = mapNodes[moveCount].GetComponent<node>().ImageIndex;
            puzzleJiyeon_renderer.sprite = jiyeonImage[temp];

            for (int i = 1; i < mapNodes.Length-2; i++)
            {
                Renderer renderer_3 = mapNodes[i].GetComponent<Renderer>();
                renderer_3.enabled = false;
            }
            if (moveCount+1 != mapNodes.Length)
            {
                Renderer renderer = mapNodes[moveCount].GetComponent<Renderer>();
                Renderer renderer_1 = mapNodes[moveCount - 1].GetComponent<Renderer>();
                Renderer renderer_2 = mapNodes[moveCount + 1].GetComponent<Renderer>();
                renderer.enabled = true;
                renderer_1.enabled = true;
                renderer_2.enabled = true;
            }
            moveCount++;
            Renderer renderer_4 = mapNodes[70].GetComponent<Renderer>();
            renderer_4.enabled = false;
        }
    }

    private IEnumerator MobMoveCoroutine()
    {
        int count = 69;
        yield return new WaitForSeconds(1f);
        while(count > 0)
        {
            if(playerMoveCount == count)
            {
                ResetPosition();
            }
            puzzle_mob.transform.position = mapNodes[count].transform.position;
            yield return new WaitForSeconds(0.4f);
            count--;
        }
        
    }

    public void ResetPosition()
    {
        puzzle_player.transform.position = mapNodes[0].transform.position;
        playerMoveCount = 0;
        StopCoroutine(MoveCoroutine());
        StopCoroutine(MobMoveCoroutine());
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
        
        for(int i = 0; i < mapNodes.Length - 1; i++)
        {
            Renderer renderer = mapNodes[i].GetComponent<Renderer>();
            renderer.enabled = true;
        }
        Renderer renderer1 = puzzle_mob.GetComponentInChildren<Renderer>();
        renderer1.enabled = true;
        door.isDoor = true;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            PlayerMove(1, 0);
            puzzlePlayer_renderer.sprite = palyerImage[3];
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PlayerMove(-1, 0);
            puzzlePlayer_renderer.sprite = palyerImage[2];
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            PlayerMove(0, -1);
            puzzlePlayer_renderer.sprite = palyerImage[0];
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            PlayerMove(0, 1);
            puzzlePlayer_renderer.sprite = palyerImage[1];
        }

        if(playerMoveCount == 69 && !isTurn)
        {
            isTurn = true;
            SetActiveNode();
            StartCoroutine(MobMoveCoroutine());
        }

        if(playerMoveCount == 0 && isTurn)
        {
            SceneManager.LoadScene("Game_4");
        }
    }


}
