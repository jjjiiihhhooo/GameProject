using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public bool isChat = false;
    public bool isChat2 = false;
    public bool isWait;

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererChatWindow;

    private List<string> listSentences;
    private List<Sprite> listSprite;
    private List<Sprite> listChatWindows;

    [SerializeField] private int[] sprite_count;
    [SerializeField] private TestChat testChat;

    public int count;

    public Animator animSprite;
    public Animator animChatWindow;

    private void Start()
    {
        count = 0;

        text.text = "";
        listSentences = new List<string>();
        listSprite = new List<Sprite>();
        listChatWindows = new List<Sprite>();
    }

    public void ShowChat(Chat chat, int[] _count) // 첫 대화를 출력
    {
        for(int i = 0; i < chat.sentences.Length; i++) // 대화 내용 초기화
        {
            listSentences.Add(chat.sentences[i]);
            listSprite.Add(chat.sprites[i]);
            listChatWindows.Add(chat.chatWindows[i]);
            sprite_count[i] = _count[i];
        }
        if (sprite_count[0] != 0) // 처음 일러가 하나니까 지연으로 설정되어 있으면 변경
        {
            animSprite.SetBool("Appear_jiyeon", true);
            animChatWindow.SetBool("Appear", true);
            StartCoroutine(ChatOpenCoroutine());
        }
        else // 아니면 그냥 출력
        {
            animSprite.SetBool("Appear", true);
            animChatWindow.SetBool("Appear", true);
            StartCoroutine(ChatOpenCoroutine());
        }
        
    }

    public void ExitChat()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprite.Clear();
        listChatWindows.Clear();
        animSprite.SetBool("Appear", false);
        animSprite.SetBool("Appear_jiyeon", false);
        animSprite.SetBool("Change", true);
        animSprite.SetBool("Change_jiyeon", true);
        animChatWindow.SetBool("Appear", false);
    }

    IEnumerator ChatOpenCoroutine()
    {
        if (count > 0)
        {
            if (listChatWindows[count] != listChatWindows[count - 1]) //?
            {
                if (sprite_count[count] != 0)
                {
                    animSprite.SetBool("Change", true);
                    animSprite.SetBool("Appear", false);
                    animSprite.SetBool("Appear_jiyeon", true);
                    animSprite.SetBool("Change_jiyeon", true);
                    animChatWindow.SetBool("Appear", false);
                    yield return new WaitForSeconds(0.2f);
                    rendererChatWindow.GetComponent<SpriteRenderer>().sprite = listChatWindows[count];
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                    animChatWindow.SetBool("Appear", true);
                    animSprite.SetBool("Change_jiyeon", false);
                }
                else
                {
                    animSprite.SetBool("Change_jiyeon", true);
                    animSprite.SetBool("Appear_jiyeon", false);
                    animSprite.SetBool("Appear", true);
                    animSprite.SetBool("Change", true);
                    animChatWindow.SetBool("Appear", false);
                    yield return new WaitForSeconds(0.2f);
                    rendererChatWindow.GetComponent<SpriteRenderer>().sprite = listChatWindows[count];
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                    animChatWindow.SetBool("Appear", true);
                    animSprite.SetBool("Change", false);
                }
            }
            else
            {
                if (listSprite[count] != listSprite[count - 1])
                {
                    if (sprite_count[count] != 0)
                    {
                        animSprite.SetBool("Change", true);
                        animSprite.SetBool("Appear", false);
                        animSprite.SetBool("Appear_jiyeon", true);
                        animSprite.SetBool("Change_jiyeon", true);
                        yield return new WaitForSeconds(0.1f);
                        rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                        animSprite.SetBool("Change_jiyeon", false);
                    }
                    else
                    {
                        animSprite.SetBool("Change_jiyeon", true);
                        animSprite.SetBool("Appear_jiyeon", false);
                        animSprite.SetBool("Appear", true);
                        animSprite.SetBool("Change", true);
                        yield return new WaitForSeconds(0.1f);
                        rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                        animSprite.SetBool("Change", false);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else // if (count == 0)
        {
            yield return new WaitForSeconds(0.05f);
            rendererChatWindow.GetComponent<SpriteRenderer>().sprite = listChatWindows[count];
            rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
        }

        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i];
            yield return new WaitForSeconds(0.01f);
        }

        isChat2 = true;
        isWait = false;

        isChat2 = true; //���� �ø��� ���� ����.

    }

    private void Update()
    {
        if(!isWait && isChat2 && Input.GetKeyDown(KeyCode.Space))
        {
            isWait = true;
            count++;
            text.text = "";

            if(count == listSentences.Count)
            {
                if(count > 13)
                {
                    testChat.MapChange();
                }
                StopAllCoroutines();
                ExitChat();
                isChat2 = false;
                isChat = true;
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(ChatOpenCoroutine());
            }
        }
    }
}
