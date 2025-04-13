public class FireExit : Interactable
{
    FireExit() : base()
    {
        this.SetName("Fire Exit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance().SetFlag("fireAlarmDisabled", false);
        this.RegisterAction("go through", GoThrough);
        this.RegisterAction("open", GoThrough);
        this.RegisterAction("use", GoThrough);
        this.RegisterAction("enter", GoThrough);
    }

    private void GoThrough(IDCard id)
    {
        if (GameManager.Instance().GetFlag("fireAlarmDisabled"))
        {
            GameManager.Instance().SetFlag("gameOver", true);
            GameManager.Instance().SetFlag("escaped", true);
            if (ThiefEnding(id) || DoctorEnding(id) || PatientEnding(id) || BrawlerEnding(id)) { return; } //If they have the thief id equipped and the ending is possible, leave.
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("go through", id));

            // Update the save data with the ending.
            SaveData saveData = FindAnyObjectByType<SaveDataManager>().LoadGame();
            if (id.Name == IDCard.Doctor.Name)
            {
                saveData.endingIAMX["Doctor"] = true;
            }
            else if (id.Name == IDCard.Patient.Name)
            {
                saveData.endingIAMX["Patient"] = true;
            }
            else if (id.Name == IDCard.Brawler.Name)
            {
                saveData.endingIAMX["Brawler"] = true;
            }
            else if (id.Name == IDCard.Thief.Name)
            {
                saveData.endingIAMX["Thief"] = true;
            }
            else if (id.Name == IDCard.Guard.Name)
            {
                saveData.endingIAMX["Guard"] = true;
            }
            FindAnyObjectByType<SaveDataManager>().SaveGame(saveData);
        }
        else
        {
            GameManager.Instance().AddTextToJournal("It's clearly written. Alarm will sound if door opened. No way I can leave through this and not get caught, unless I can figure out how to disable the alarms.");
        }
    }
    private bool ThiefEnding(IDCard id)
    {
        if (!GameManager.Instance().GetFlag("Thief Ending") || id.Name != IDCard.Thief.Name)
        {
            return false;
        }
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("thief ending", id));
        SaveData saveData = FindAnyObjectByType<SaveDataManager>().LoadGame();
        saveData.endingThief = true;
        FindAnyObjectByType<SaveDataManager>().SaveGame(saveData);
        return true;
    }

    private bool DoctorEnding(IDCard id)
    {
        if (id.Name == IDCard.Doctor.Name && GameManager.Instance().GetFlag("hasPunchedPunchcard") && GameManager.Instance().GetFlag("LabCoat") && FindFirstObjectByType<PlayerInfo>().isItemInInventory("PhD"))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("doctor ending", id));
            SaveData saveData = FindAnyObjectByType<SaveDataManager>().LoadGame();
            saveData.endingDoctor = true;
            FindAnyObjectByType<SaveDataManager>().SaveGame(saveData);
            return true;
        }
        else
        {
            return false;
        }
    }

    // Make sure to implement in FireExit.cs aswell
    private bool BrawlerEnding(IDCard id)
    {
        if (id.Equals(IDCard.Brawler) && GameManager.Instance().GetFlag("booze") && FindAnyObjectByType<PlayerInfo>().isItemInInventory("Gauze Fist Wraps"))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("brawler ending", id));
            SaveData saveData = FindAnyObjectByType<SaveDataManager>().LoadGame();
            saveData.endingBrawler = true;
            FindAnyObjectByType<SaveDataManager>().SaveGame(saveData);
            return true;
        }
        return false;
    }

    private bool PatientEnding(IDCard id)
    {
        if (id.Name == IDCard.Patient.Name && GameManager.Instance().GetFlag("PatientFileRead") && GameManager.Instance().GetFlag("releaseSlip"))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("patient ending", id));
            SaveData saveData = FindAnyObjectByType<SaveDataManager>().LoadGame();
            saveData.endingRediscoverYourself = true;
            FindAnyObjectByType<SaveDataManager>().SaveGame(saveData);
            return true;
        }
        else
        {
            return false;
        }
    }
}
