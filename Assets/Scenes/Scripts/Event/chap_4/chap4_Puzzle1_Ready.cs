using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chap4_Puzzle1_Ready : MonoBehaviour
{
    /* 
     * 검은 형체를 생성하고 대화창 실행 ( 헉... )
     * 이후, 퍼즐 준비 완료
     */
    [SerializeField] Transform O_point; // 좌표 좌측 상단
    [SerializeField] Transform P_point; // 좌표 우측 하단
    [SerializeField] GameObject eyeSpawn;
    [SerializeField] GameObject JY_0;
    [SerializeField] GameObject JY_1;
    chap4_MapSpawner mapSpawner;
    DialogueBox dialogueBox;

    // 맵의 가로 세로 폭
    float width;
    float height;

    int[,] map = { // 검 // 낮 1
    { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0},
    { 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0},
    { 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0},
    { 1, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0},
    { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0},
    { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1},
    { 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1},
    { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    { 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    { 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 1, 1},
    { 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0},
    { 0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0},
    { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0}
};

    void Start()
    {
        dialogueBox = GetComponent<DialogueBox>();
        mapSpawner = GetComponent<chap4_MapSpawner>();
        StartCoroutine(AppearBlackShape(2.0f));

        width = (Mathf.Abs(O_point.position.x - P_point.position.x) / (15));
        height = (Mathf.Abs(O_point.position.y - P_point.position.y) / (11));
    }

    IEnumerator AppearBlackShape(float _waitTime)
    {
        yield return new WaitForSeconds(1.0f);
        // blackShape.SetActive(true);
        List<GameObject> mapInstance = new List<GameObject>();
        for (int x = 0; x < 16; x++)
        {
            for (int y = 0; y < 12; y++)
            {
                mapInstance.Add(Instantiate(eyeSpawn, new Vector2(O_point.position.x + x * width, O_point.position.y - y * height), Quaternion.identity));
            }
        }
        yield return new WaitForSeconds(_waitTime);

        dialogueBox.SetDialogue(); // 헉...
        while(true)
        {
            if (dialogueBox.noMore && !dialogueBox.isLog)
            {
                JY_0.SetActive(false);
                JY_1.SetActive(true);
                mapSpawner.SetGetReady(true);
                foreach (GameObject toDestroy in mapInstance) Destroy(toDestroy);
                mapInstance.Clear();
                break;
            }
            yield return null;
        }
    }
}