using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;
using System.Collections;

public class JournalOutput : MonoBehaviour
{

    private static JournalOutput instance;

    private TMP_Text viewableText;
    private ScrollRect scrollRect;

    private string currentEntry = "";
    private string history = "";
    private bool allowTyping = true;

    public static JournalOutput GetInstance()
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
    }

    private void FixedUpdate()
    {
        history.TrimStart('\n');
        history.Trim();
        viewableText.text = history + $"{currentEntry}\n\n";
        UpdateScrollPosition();
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
        StartCoroutine(AddGameTextSlowly(text));
    }

    IEnumerator AddGameTextSlowly(string text)
    {
        if (text.Length == 0)
        {
            yield break;
        }
        allowTyping = false;
        float startTime = Time.time;
        int charPerSec = 50;
        for (int i = 0; i < text.Length;)
        {
            float timeSince = Time.time - startTime;
            if ((float)i / timeSince < charPerSec)
            {
                history += text[i];
                i++;
            }
            yield return null;
        }
        history += "\n\n";
        allowTyping = true;
    }

    private void UpdateScrollPosition()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type != EventType.KeyDown || !allowTyping)
        {
            return;
        }

        if (e.keyCode == KeyCode.Return)
        {
            if (currentEntry.Length <= 0)
            {
                return;
            }
            // Pass Entry off to GameManager
            GameManager.GetInstance().handlePlayerInput(currentEntry);
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
