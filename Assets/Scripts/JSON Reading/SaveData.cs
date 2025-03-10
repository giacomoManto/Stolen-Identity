using UnityEngine;


[System.Serializable]
public class SaveData
{
    public SerializableDictionary<string, bool> endingIAMX;
    public bool endingRediscoverYourself;
    public bool endingDoctor;
    public bool endingThief;
    public bool endingBrawler;

    public string playerName;


    public SaveData()
    {
        endingIAMX = new SerializableDictionary<string, bool>();
        endingIAMX.Add("Patient", false);
        endingIAMX.Add("Doctor", false);
        endingIAMX.Add("Thief", false);
        endingIAMX.Add("Brawler", false);

        endingRediscoverYourself = false;
        endingDoctor = false;
        endingThief = false;
        endingBrawler = false;
        playerName = "";
    }
}
