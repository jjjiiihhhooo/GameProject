using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_MapSpawner : MonoBehaviour
{
    [SerializeField] Transform O_point; // ÁÂÇ¥ ÁÂÃø »ó´Ü
    [SerializeField] Transform P_point; // ÁÂÇ¥ ¿ìÃø ÇÏ´Ü
    [SerializeField] GameObject obstacle;
    [SerializeField] GameObject wall;
    [SerializeField] GameObject blink;
    chap4_PlayerMove_Puzzle1 PM;
    bool getReady;
    bool singleCall = true;

    // ¸ÊÀÇ °¡·Îx¼¼·Î °ª
    [SerializeField] int X;
    [SerializeField] int Y;
    // ¸ÊÀÇ °¡·Î ¼¼·Î Æø
    float width;
    float height;

    // ¸Ê, ¸Ê ¹è¿­
    int[,] map;
    [SerializeField] int[] mapArray;
    int i = 0;

    int[,] map1 = { // ÃÊ
        {1, 1, 1, 1, 1, 1, 0, 0, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1},
        {1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 1, 2, 1, 1},
        {2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 0, 1, 2, 2, 2, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 2, 1, 1, 1, 1},
        {2, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2},
        {1, 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 2, 1, 0, 0, 2, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2},
        {1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
        {1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 0, 0, 0, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
    };

    int[,] map2 = { // °Ë
        {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 0, 0, 2, 1, 1, 1, 2, 2, 2, 0, 0, 1, 1},
        {1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0, 1, 1},
        {2, 2, 2, 0, 0, 1, 1, 1, 1, 1, 2, 1, 2, 2, 2, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 2, 1, 1, 1, 1},
        {2, 2, 2, 1, 1, 1, 2, 2, 2, 0, 0, 1, 2, 2, 2, 2, 2},
        {1, 1, 0, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 0, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 0, 0, 0, 0},
        {1, 1, 0, 1, 2, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1},
        {1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
    };

    int[,] map3 = { // º¸
        {1, 1, 1, 1, 1, 1, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 0, 0, 0, 0, 2, 1, 1},
        {1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 0, 1, 1, 1, 2, 1, 1},
        {2, 0, 0, 0, 2, 1, 1, 1, 1, 1, 2, 1, 0, 0, 2, 1, 1},
        {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0, 1, 1, 1, 1},
        {2, 2, 2, 1, 1, 1, 0, 0, 2, 2, 2, 1, 0, 0, 2, 2, 2},
        {1, 1, 2, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 2, 1, 2, 0, 0, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 0, 1, 2, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
        {1, 1, 0, 0, 2, 1, 1, 1, 1, 1, 1, 1, 0, 0, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
    };

    int[,] map4 = { // ³ë
        {1, 1, 1, 1, 1, 1, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 1, 1, 1, 2, 0, 0, 1, 1, 1, 2, 2, 2, 2, 2, 1, 1},
        {1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0, 1, 1},
        {0, 0, 2, 2, 2, 1, 1, 1, 1, 1, 2, 1, 2, 0, 0, 1, 1},
        {0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1, 1, 1, 1},
        {0, 0, 2, 1, 1, 1, 2, 0, 0, 0, 2, 1, 2, 0, 0, 0, 0},
        {1, 1, 2, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
        {1, 1, 2, 1, 2, 2, 2, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 2, 1, 0, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1},
        {1, 1, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2},
    };


    void Start()
    {
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
            StartCoroutine(DrawMap(mapArray[i]));
        }
    }

    IEnumerator DrawMap(int mapNum)
    {
        if (mapNum <= 0 && mapNum > 4)
        {
            Debug.Log("¸Ê ÀÎµ¦½º°¡ ¹üÀ§ ¹Ù±ù¿¡ ÀÖ½À´Ï´Ù.");
            yield break;
        }


        // »ý¼ºÇÒ ¸Ê ÃÊ±âÈ­
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

        // ¾ÏÀü
        blink.SetActive(true);
        yield return new WaitUntil(() => !blink.activeSelf);

        // ¸Ê »ý¼º
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
                    mapInstance.Add(Instantiate(obstacle, new Vector2(O_point.position.x + x * width, O_point.position.y - y * height), Quaternion.identity));
                }
            }
        }
        yield return new WaitForSeconds(2.0f);

        // ¸Ê »èÁ¦
        foreach (GameObject toDestroy in mapInstance) Destroy(toDestroy);
        mapInstance.Clear();

        // ¸Ê ¹è¿­ ÀÎµ¦½º ÃÊ±âÈ­
        i++;
        if (i >= mapArray.Length)
        {
            i = -1;
            getReady = false;
        }

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

    // ÇÁ·ÎÆÛÆ¼
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
