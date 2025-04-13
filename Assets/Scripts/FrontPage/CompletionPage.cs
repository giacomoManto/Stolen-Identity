using UnityEngine;
using TMPro;

public class CompletionPage : MonoBehaviour
{

    [SerializeField]
    TMP_Text completionText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayText()
    {
        SaveData data = FindAnyObjectByType<SaveDataManager>().LoadGame();
        string text = "<size=36><align=center>Endings Completed</align></size>\n";
        text += "\n<align=left>Generic Endings";
        foreach (var item in data.endingIAMX)
        {
            if (item.Value)
            {
                text += "\n" + item.Key + ": Completed";
            }
            else
            {
                text += "\n?????????: Not Completed";
            }
        }
        text += "\n\nUnique Endings";
        if (data.endingRediscoverYourself)
        {
            text += "\nRediscover Yourself: Completed";
        }
        else
        {
            text += "\n?????????: Not Completed";
        }
        if (data.endingDoctor)
        {
            text += "\nBecome a Doctor: Completed";
        }
        else
        {
            text += "\n?????????: Not Completed";
        }
        if (data.endingThief)
        {
            text += "\nBecome a Thief: Completed";
        }
        else
        {
            text += "\n?????????: Not Completed";
        }
        if (data.endingBrawler)
        {
            text += "\nBecome a Brawler: Completed";
        }
        else
        {
            text += "\n?????????: Not Completed";
        }
        text += "</align>";

        completionText.text = text;
    }
}
