using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_MapSpawner : MonoBehaviour
{
    GameObject player;
    bool singleCall_1 = true;
    [SerializeField] Transform O_point; // 좌표 좌측 상단
    [SerializeField] Transform P_point; // 좌표 우측 하단
    [SerializeField] GameObject blackShape;
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject blink;
    [SerializeField] GameObject night;
    chap4_PlayerMove_Puzzle1 PM;
    bool getReady;
    bool singleCall = true;

    // 맵의 가로x세로 값
    [SerializeField] int X;
    [SerializeField] int Y;
    // 맵의 가로 세로 폭
    float width;
    float height;

    // 맵, 맵 배열
    int[,] map;
    [SerializeField] int[] mapArray0;
    [SerializeField] int[] mapArray1;
    int i = 0;

    // 챕터 3의 선택에 따라 달라짐 ( 같이 나갈 경우 true )
    [SerializeField] bool getJYin3 = true;
    float wait = 2.0f;
    int[] mapArray;

    int[,] map1 = { // 검
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

    int[,] map2 = { // 초
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

    int[,] map3 = { // 보
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

    int[,] map4 = { // 노
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

            // 챕터 3에서 지연 나갔을 경우 true
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
            Debug.Log("맵 인덱스가 범위 바깥에 있습니다.");
            yield break;
        }


        // 생성할 맵 초기화
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

        // 암전
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


        // 맵 생성
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
                    // 맵 생성시, 플레이어 위치에는 함정생성x
                    if (PM.PosX == x && PM.PosY == y)
                        continue;
                    mapInstance.Add(Instantiate(obstacle, new Vector2(O_point.position.x + x * width, O_point.position.y - y * height), Quaternion.identity));
                }
            }
        }
        yield return new WaitForSeconds(wait);

        // 맵 삭제
        foreach (GameObject toDestroy in mapInstance) Destroy(toDestroy);
        mapInstance.Clear();

        // 맵 배열 인덱스 초기화
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

    // 프로퍼티
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
