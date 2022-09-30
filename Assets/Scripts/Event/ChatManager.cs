using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public bool isChat = false;
    public bool isChat2 = false;

    public static ChatManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    } //ΩÃ±€≈Ê

    public Text text;
    public SpriteRenderer rendererSprite;
    public SpriteRenderer rendererChatWindow;

    private List<string> listSentences;
    private List<Sprite> listSprite;
    private List<Sprite> listChatWindows;

    [SerializeField] private int count;

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

    public void ShowChat(Chat chat)
    {
        for(int i = 0; i < chat.sentences.Length; i++)
        {
            listSentences.Add(chat.sentences[i]);
            listSprite.Add(chat.sprites[i]);
            listChatWindows.Add(chat.chatWindows[i]);
        }

        animSprite.SetBool("Appear", true);
        animChatWindow.SetBool("Appear", true);
        StartCoroutine(ChatOpenCoroutine());
    }

    public void ExitChat()
    {
        text.text = "";
        count = 0;
        listSentences.Clear();
        listSprite.Clear();
        listChatWindows.Clear();
        animSprite.SetBool("Appear", false);
        animSprite.SetBool("Change", true);
        animChatWindow.SetBool("Appear", false);
    }

    IEnumerator ChatOpenCoroutine()
    {
        if(count > 0)
        {
            if (listChatWindows[count] != listChatWindows[count - 1])
            {
                animSprite.SetBool("Change", true);
                animChatWindow.SetBool("Appear", false);
                yield return new WaitForSeconds(0.2f);
                rendererChatWindow.GetComponent<SpriteRenderer>().sprite = listChatWindows[count];
                rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                animChatWindow.SetBool("Appear", true);
                animSprite.SetBool("Change", false);
            }
            else
            {
                if (listSprite[count] != listSprite[count - 1])
                {
                    animSprite.SetBool("Change", true);
                    yield return new WaitForSeconds(0.1f);
                    rendererSprite.GetComponent<SpriteRenderer>().sprite = listSprite[count];
                    animSprite.SetBool("Change", false);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        else
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
    }

    private void Update()
    {
        if(isChat2 && Input.GetKeyDown(KeyCode.Space))
        {
            count++;
            text.text = "";

            if(count == listSentences.Count)
            {
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
