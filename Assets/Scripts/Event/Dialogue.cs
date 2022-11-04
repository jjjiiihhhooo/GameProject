using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    /*
     *  choice의 값에 따라서 대화 / 선택을 분류해서 출력
     *  choice의 질문은 대화창과 단일화를 할 것이므로, 제거
     *  따라서 선택창 구현시, 대화(질문) -> 선택으로 나누어 구현해야함.
     *  한 대화에 모든 요소를 설정할 것이기에, 배열로 선언하지 않음. 
     *  대신 Dialogue가 배열로 선언될 것.
     */

    // 대화 요소
    public Sprite charSprites; // 캐릭터 스프라이트
    public int charNum; // 화자. 하나는 0, 지연은 1.
    [TextArea(1, 2)]
    public string sentences;

    // 선택 요소
    public bool choice; // 현제 대화 or 선택 중 무엇인지.
    public string answers_0;
    public string answers_1;
}
