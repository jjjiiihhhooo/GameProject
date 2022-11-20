using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_MapSpawner : MonoBehaviour
{
    GameObject player;
    bool singleCall_1 = true;
    [SerializeField] Transform O_point; // ��ǥ ���� ���
    [SerializeField] Transform P_point; // ��ǥ ���� �ϴ�
    [SerializeField] GameObject blackShape;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject blink;
    [SerializeField] GameObject night;
    chap4_PlayerMove_Puzzle1 PM;
    bool getReady;
    bool singleCall = true;

    // ���� ����x���� ��
    [SerializeField] int X;
    [SerializeField] int Y;
    // ���� ���� ���� ��
    float width;
    float height;

    // ��, �� �迭
    int[,] map;
    [SerializeField] int[] mapArray0;
    [SerializeField] int[] mapArray1;
    int i = 0;

    // é�� 3�� ���ÿ� ���� �޶��� ( ���� ���� ��� true )
    [SerializeField] bool getJYin3 = true;
    float wait = 2.0f;
    int[] mapArray;

    int[,] map1 = { // ��
        {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 2, 1, 1},
        {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 2, 1, 1},
        {2, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1, 2, 2, 2, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 2, 1, 1, 1, 1},
        {2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2},
        {1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 2, 1, 0, 0, 2, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2},
        {1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
        {1, 1, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2}
    };

    int[,] map2 = { // ��
        {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 2, 2, 2, 0, 0, 1, 1},
        {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0, 1, 1},
        {2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1},
        {2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2},
        {1, 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 0, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 0, 0, 0, 0},
        {1, 1, 0, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1},
        {1, 1, 0, 0, 2, 1, 1, 1, 1, 1, 1, 1, 2, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0}
    };

    int[,] map3 = { // ��
        {1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1},
        {1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0, 1, 1},
        {2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 0, 0, 0, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0, 1, 1, 1, 1},
        {2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 2, 1, 0, 0, 2, 2, 2},
        {1, 1, 2, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 2, 1, 2, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1},
        {1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2}
    };

    int[,] map4 = { // ��
        {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1},
        {1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 1},
        {0, 0, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 1, 1},
        {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 2, 1, 1, 1, 1},
        {0, 0, 0, 1, 1, 1, 2, 2, 2, 0, 0, 1, 2, 0, 0, 0, 0},
        {1, 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 2, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1},
        {1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2}
    };


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PM = GetComponent<chap4_PlayerMove_Puzzle1>();
        map = new int[Y, X];
        width = (Mathf.Abs(O_point.position.x - P_point.position.x) / (X - 1));
        height = (Mathf.Abs(O_point.position.y - P_point.position.y) / (Y - 1));
    }

    void Update()
    {
        if (singleCall && getReady)
        {
            singleCall = false;

            // é�� 3���� ���� ������ ��� true
            if(getJYin3)
            {
                mapArray = mapArray0;
                wait = 2.0f;
            }
            else
            {
                mapArray = mapArray1;
                wait = 1.0f;
            }
            StartCoroutine(DrawMap(mapArray[i%mapArray.Length]));
        }
    }

    IEnumerator DrawMap(int mapNum)
    {
        ///
        if (singleCall_1)
        {
            singleCall_1 = false;
            player.transform.position = P_point.position + new Vector3(0, 0.925f, 0);
            player.GetComponent<PlayerMove>().inEvent = true;
        }

        if (mapNum <= 0 && mapNum > 4)
        {
            Debug.Log("�� �ε����� ���� �ٱ��� �ֽ��ϴ�.");
            yield break;
        }


        // ������ �� �ʱ�ȭ
        switch (mapNum)
        {
            case 1:
                map = map1;
                break;
            case 2:
                map = map2;
                break;
            case 3:
                map = map3;
                break;
            case 4:
                map = map4;
                break;
        }

        // ����
        if (i % 2 == 0)
        {
            night.SetActive(true);
            if(!blackShape.activeSelf) blackShape.SetActive(true);
        }
        else if (i % 2 == 1)
        {
            night.SetActive(false);
            if (blackShape.activeSelf) blackShape.SetActive(false);
        }    


        // �� ����
        List<GameObject> mapInstance = new List<GameObject>();
        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                if (map[y, x] == 1)
                {
                    mapInstance.Add(Instantiate(wall, new Vector2(O_point.position.x + x * width, O_point.position.y - y * height), Quaternion.identity));
                }
                else if (map[y, x] == 2)
                {
                    // �� ������, �÷��̾� ��ġ���� ��������x
                    if (PM.PosX == x && PM.PosY == y)
                        continue;
                    mapInstance.Add(Instantiate(obstacle, new Vector2(O_point.position.x + x * width, O_point.position.y - y * height), Quaternion.identity));
                }
            }
        }
        yield return new WaitForSeconds(wait);

        // �� ����
        foreach (GameObject toDestroy in mapInstance) Destroy(toDestroy);
        mapInstance.Clear();

        // �� �迭 �ε��� �ʱ�ȭ
        i++;
        
        //if (i >= mapArray.Length)
        //{
        //    i = -1;
        //    getReady = false;
        //}

        singleCall = true;
    }

    public bool GetGetReady()
    {
        return getReady;
    }

    public void SetGetReady(bool _bool)
    {
        getReady = _bool;
    }

    // ������Ƽ
    public int x
    {
        get { return X; }
    }
    public int y
    {
        get { return Y; }
    }
    public int[,] Map
    {
        get { return map; }
    }
}
