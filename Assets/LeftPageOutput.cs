using TMPro;
using UnityEngine;

public class LeftPageOutput : MonoBehaviour
{

    private static LeftPageOutput instance;

    private TMP_Text viewableText;
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
    }
    public static LeftPageOutput Instance()
    {
        return instance;
    }

    public void updatePage(string roomName)
    {
        viewableText.text = roomName;
    }
}
