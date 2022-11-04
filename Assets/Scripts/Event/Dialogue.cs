using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    /*
     *  choice�� ���� ���� ��ȭ / ������ �з��ؼ� ���
     *  choice�� ������ ��ȭâ�� ����ȭ�� �� ���̹Ƿ�, ����
     *  ���� ����â ������, ��ȭ(����) -> �������� ������ �����ؾ���.
     *  �� ��ȭ�� ��� ��Ҹ� ������ ���̱⿡, �迭�� �������� ����. 
     *  ��� Dialogue�� �迭�� ����� ��.
     */

    // ��ȭ ���
    public Sprite charSprites; // ĳ���� ��������Ʈ
    public int charNum; // ȭ��. �ϳ��� 0, ������ 1.
    [TextArea(1, 2)]
    public string sentences;

    // ���� ���
    public bool choice; // ���� ��ȭ or ���� �� ��������.
    public string answers_0;
    public string answers_1;
}
