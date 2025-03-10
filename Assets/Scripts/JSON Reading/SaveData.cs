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
        // Add endings

        endingRediscoverYourself = false;
        endingDoctor = false;
        endingThief = false;
        endingBrawler = false;
        playerName = "";
    }
}
