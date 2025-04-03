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
    SaveData gameData;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(TalkTuah, "say hello to", "talk", "hello", "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
        gameData = SaveDataManager.Instance().LoadGame();
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
        string dialogue;
        switch (talkCount[id])
        {
            case (0):
                dialogue = GetTextFromJson("talk0", id);
                this.locationDescription = realDescription;
                this.SetName(realName);
                this.GetComponentInParent<RoomBehavior>().InitInteractables();
                break;
            case (1):
                dialogue = GetTextFromJson("talk1", id);
                break;
            default:
                dialogue = GetTextFromJson("talkDefault", id);
                if (id.Name == IDCard.Patient.Name)
                {
                    GameManager.Instance().SetFlag("discoveredName", true);
                }
                break;
        }
        GameManager.Instance().AddTextToJournal(dialogue.Replace("{name}", gameData.playerName));

    }
}
