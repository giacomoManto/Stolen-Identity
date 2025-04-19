using UnityEngine;

public class Doctor : Interactable
{
    [SerializeField]
    GameObject releaseSlip;
    SaveData gameData;
    void Start()
    {
        this.RegisterAction(TalkTuah, "say hello to", "talk", "hello", "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
        this.RegisterAction("use", Use);
        gameData = SaveDataManager.Instance().LoadGame();
        if (releaseSlip == null)
        {
            throw new System.Exception("Release slip not assigned in the inspector.");
        }
    }

    private void TalkTuah(IDCard id)
    {
        if (id.Name == IDCard.Patient.Name)
        {
            if (GameManager.Instance().GetFlag("releaseSlip"))
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talkReleased", id));
            }
            else if (GameManager.Instance().GetFlag("discoveredName"))
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talkKnow", id).Replace("{name}", gameData.playerName));
                GameManager.Instance().SetFlag("releaseSlip", true);
                GameManager.Instance().addObjectToPlayerInventory(releaseSlip.GetComponent<Interactable>());
                return;
            }
            else
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson("talk", id));
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("talk", id));
        }
    }

    // Cheap workaround to be able to use the doctor id card in presence of doctor.
    private void Use(IDCard id)
    {
        FindFirstObjectByType<PlayerInfo>().switchPlayerID(IDCard.Doctor.Name);
    }
}
