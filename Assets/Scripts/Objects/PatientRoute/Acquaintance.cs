using System.Collections.Generic;
using UnityEngine;

public class Acquaintance : Interactable
{
    [SerializeField]
    [Tooltip("The real name of the acquaintance MAKE SURE TO UPDATE DIALOGUE IF YOU CHANGE THIS")]
    private string realName;
    [SerializeField]
    private string realDescription;

    private Dictionary<IDCard, int> talkCount = new Dictionary<IDCard, int>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(TalkTuah, "say hello to", "talk", "hello", "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
    }

    private void TalkTuah(IDCard id)
    {
        if (talkCount.ContainsKey(id))
        {
            talkCount[id] += 1;
        }
        else
        {
            talkCount[id] = 0;
        }
        switch (talkCount[id])
        {
            case (0):
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talk0", id));
                this.locationDescription = realDescription;
                this.GetComponentInParent<RoomBehavior>().InitInteractables();
                break;
            case (1):
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talk1", id));
                break;
            default:
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talkDefault", id));
                break;
        }

    }

    private string ReplaceName(string originalText)
    {
        SaveData savedata = SaveDataManager.Instance().LoadGame();
        return originalText;
    }
}
