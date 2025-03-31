public class PatientFile : Interactable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RegisterAction(Read, "read", "interpret", "analyze", "examine", "review", "study", "peruse", "scan", "inspect", "decipher", "assess");
    }

    private void Read(IDCard id)
    {
        GameManager.Instance().AddTextToJournal(GetTextFromJson("read", id));

        if (id.Name == IDCard.Doctor.Name)
        {
            GameManager.Instance().SetFlag("PatientFileRead", true);
        }
    }
}
