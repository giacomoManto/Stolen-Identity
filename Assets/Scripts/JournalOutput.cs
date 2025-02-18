using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;

public class JournalOutput : MonoBehaviour
{

    [SerializeField]
    private int standardCharPerSec = 50;
    [SerializeField]
    private int fastCharPerSec = 100;

    private int charPerSec;



    private static JournalOutput instance;

    private TMP_Text viewableText;
    private ScrollRect scrollRect;

    private string currentEntry = "";
    private string history = "";
    private bool allowTyping = true;
    private bool textDisplayRunning = false;

    private Queue<string> gameTextQueue = new Queue<string>();

    public static JournalOutput Instance()
    {
        return instance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        viewableText = GetComponentInChildren<TMP_Text>();
        scrollRect = GetComponent<ScrollRect>();
        charPerSec = standardCharPerSec;
    }

    private void FixedUpdate()
    {
        history.TrimStart('\n');
        history.Trim();
        viewableText.text = history + $"{currentEntry}\n\n";
        UpdateScrollPosition();

        if (gameTextQueue.Count > 0 && !textDisplayRunning)
        {
            textDisplayRunning = true;
            StartCoroutine(AddGameTextSlowly(gameTextQueue.Dequeue()));
        }
    }

    private void Start()
    {
    }

    public void Clear()
    {
        history = "";
        currentEntry = "";
    }

    public void AddGameText(string text)
    {
        gameTextQueue.Enqueue(text);
    }

    private IEnumerator AddGameTextSlowly(string text)
    {
        if (text.Length == 0)
        {
            yield break;
        }
        allowTyping = false;
        float startTime = Time.time;
        for (int i = 0; i < text.Length;)
        {
            float timeSince = Time.time - startTime;
            if ((float)i / timeSince < charPerSec)
            {
                // Skip all html business
                if (text[i] == '<')
                {
                    while (text[i] != '>')
                    {
                        history += text[i];
                        if (i >= text.Length)
                        {
                            throw new System.Exception("Invalid HTML in game text");
                        }
                        i++;
                    }
                    history += text[i];
                    i++;
                }
                else {
                    history += text[i];
                    i++;
                }
            }
            yield return null;
        }
        history += "\n\n";
        allowTyping = true;
        textDisplayRunning = false;
        charPerSec = standardCharPerSec;
    }

    private void UpdateScrollPosition()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
        {
            return;
        }
        else if (!allowTyping)
        {
            if (textDisplayRunning)
            {
                charPerSec = fastCharPerSec; // Speed up text display}
                return;
            }
            else
            {
                return;
            }
        }

        if (textDisplayRunning)
        {
            
        }

        if (e.keyCode == KeyCode.Return)
        {
            if (currentEntry.Length <= 0)
            {
                return;
            }
            // Pass Entry off to GameManager
            GameManager.Instance().handlePlayerInput(currentEntry);
            currentEntry = "";
        }
        else if (e.keyCode == KeyCode.Backspace)
        {
            if (currentEntry.Length <= 0)
            {
                return;
            }
            currentEntry = currentEntry.Substring(0, currentEntry.Length - 1);
        }
        else
        {
            char c = e.character;
            if (!char.IsControl(c))
            {
                currentEntry += c;
            }
        }
    }

}
