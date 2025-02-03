using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JournalOutput : MonoBehaviour
{

    private static JournalOutput instance;

    private TMP_Text viewableText;
    private ScrollRect scrollRect;

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

    private void Start()
    {
    }

    public void Clear()
    {
        viewableText.text = "";
    }

    public void AddPlayerText(string text)
    {
        viewableText.text += $"<align=right>{text}\n</align>";
        UpdateScrollPosition();
    }

    public void AddGameText(string text)
    {
        viewableText.text += $"<align=left>{text}\n</align>";
        UpdateScrollPosition();
    }

    private void UpdateScrollPosition()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
    
}
