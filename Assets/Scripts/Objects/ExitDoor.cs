public class ExitDoor : Interactable
{
    ExitDoor() : base()
    {
        // Set the object's name
        SetName("Exit Door");
    }

    private void Start()
    {
        this.RegisterAction(GoThrough, "use", "open", "go through", "pass through", "enter", "move through", "walk through", "step through", "proceed through", "traverse", "penetrate", "slip through", "creep through", "push through", "burst through", "barge through", "infiltrate", "tread through");
        this.RegisterAction(InspectObject, "inspect", "look at", "peek at", "stare at");

    }
    private void GoThrough(IDCard id)
    {
        if (ThiefEnding(id) || DoctorEnding(id) || BrawlerEnding(id) || PatientEnding(id))
        {
            escapeSuccess();
            return;
        }
        else if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("go through", id));
            escapeSuccess();
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
            GameManager.Instance().AddTextToJournal("The guards won't let me through. I need to distract them or get the correct permission to get out of here.");
        }
    }

    private void escapeSuccess()
    {
        GameManager.Instance().SetFlag("gameOver", true);
        GameManager.Instance().SetFlag("escaped", true);
    }


    public override void InspectObject(IDCard id)
    {
        if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal("It's the exit door. I can see the outside world through the glass. I can't wait to get out of here. The guards are still distracted, I should make my move now.");
        }
        else
        {
            GameManager.Instance().AddTextToJournal("It's the exit door. I can see the outside world through the glass. I can't wait to get out of here. Unfortunately two guards look like they are more focused on keeping people in rather than out.");
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
        if (id.Equals(IDCard.Brawler) && GameManager.Instance().GetFlag("booze") && FindAnyObjectByType<PlayerInfo>().isItemInInventory("Gauze Fist Wraps") && FindAnyObjectByType<PlayerInfo>().isItemInInventory("Bar Rewards Card"))
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
