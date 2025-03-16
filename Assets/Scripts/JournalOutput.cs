using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;
using System.Collections.Generic;

public class JournalOutput : MonoBehaviour
{
    [Header("Text Char Speeds (multiply by 50 for char per sec)")]
    [SerializeField]
    private int standardCharSpeed = 1;
    [SerializeField]
    private int fastCharSpeed = 3;

    private int charSpeed;

    private static JournalOutput instance;

    private TMP_Text viewableText;
    private ScrollRect scrollRect;
    private int viewableChars = 0;


    private string currentEntry = "";
    private string history = "";

    private bool ready = false;
    private bool loaded = false;

    private Queue<string> gameTextQueue;
    public bool Ready
    {
        get { return ready; }
    }

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
        charSpeed = standardCharSpeed;
        gameTextQueue = new Queue<string>();
    }

    private void Start()
    {
        ready = true;
        StartCoroutine(WaitForInit());
    }

    private IEnumerator WaitForInit()
    {
        yield return new WaitUntil(() =>
            GameManager.Instance() != null);
        loaded = true;
    }

    private void FixedUpdate()
    {
        history = history.TrimStart('\n');
        if (gameTextQueue.Count > 0 && viewableChars >= viewableText.textInfo.characterCount)
        {
            history += gameTextQueue.Dequeue() + '\n' + '\n';
        }

        if (viewableChars < viewableText.textInfo.characterCount)
        {
            viewableChars += charSpeed;
            UpdateScrollPosition();
        }

        viewableText.maxVisibleCharacters = viewableChars + currentEntry.Length;

        viewableText.text = history + currentEntry;
    }

    public bool DisplayingText()
    {
        bool test = viewableChars < viewableText.textInfo.characterCount || gameTextQueue.Count > 0;
         return test;
    }

    public void Clear()
    {
        history = "";
        currentEntry = "";
        viewableChars = 0;
    }

    public void AddGameText(string text)
    {
        gameTextQueue.Enqueue(text);
    }

    private void UpdateScrollPosition()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void OnGUI()
    {
        if (!loaded)
        {
            return;
        }
        if (GameManager.Instance().GetFlag("gameOver"))
        {
            return;
        }
        Event e = Event.current;
        if(e.type == EventType.KeyDown || e.type == EventType.KeyUp)
        {
            UpdateScrollPosition();
        }
        if (e.type != EventType.KeyDown)
        {
            return;
        }
        
        if (viewableChars < viewableText.textInfo.characterCount - currentEntry.Length)
        {
            if (e.keyCode == KeyCode.Space)
            {
                viewableChars = viewableText.textInfo.characterCount - currentEntry.Length;
            }
            charSpeed = fastCharSpeed; // Speed up text display
            return;
        }
        charSpeed = standardCharSpeed;

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
                currentEntry = currentEntry.TrimStart();
            }
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

}
