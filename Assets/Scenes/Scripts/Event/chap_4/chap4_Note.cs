using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class chap4_Note
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
    public bool isNote;
    public string NoteSTC0;
    public string NoteSTC1;
    public string NoteSTC2;
    public string NoteSTC3;
    public string NoteSTC4;
    public string NoteSTC5;
}
