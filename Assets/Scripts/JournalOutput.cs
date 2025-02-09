using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class JournalOutput : MonoBehaviour
{

    private static JournalOutput instance;

    private TMP_Text viewableText;
    private ScrollRect scrollRect;

    private string currentEntry = "";
    private string history = "";

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
        viewableText.text = history + $"<align=right>{currentEntry}</align>\n";
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

    public void AddPlayerText(string text)
    {
        history += $"<align=right>{text}\n</align>";
    }

    public void AddGameText(string text)
    {
        history += $"<align=left>{text}\n</align>";
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

        if (e.keyCode == KeyCode.Return)
        {
            if (currentEntry.Length <= 0)
            {
                return;
            }
            AddPlayerText(currentEntry);
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
