using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
/// <summary>
///Formats the game data into a readable format for the save and load system.
/// </summary>
public class DialogueTemplate
{
    public SerializableDictionary<string, string> doorDialogue; //Saves all the outlets GUID's and clean energy values as a percent 0-1.0f
    public SerializableDictionary<string, string> roomDialogue; //Saves all the outlets GUID's and virus energy values as a percent 0-1.0f
    public SerializableDictionary<string, string> objectDialogue;   //Saves all the outlets GUID's and max energy values as a percent 0-1.0f

    /**
     * When a new game is created, all values in the constructor should be read into the game
     * This will give the player the below starting values.
     */
    public DialogueTemplate()
    {
        doorDialogue = new SerializableDictionary<string, string>();
        doorDialogue["doorDialogue Key"] = "Hello Value";
        doorDialogue["doorDialogue 2nd Key"] = "2nd Value";
        roomDialogue = new SerializableDictionary<string, string>();
        roomDialogue["roomDialogue Key"] = "Hello Value";
        objectDialogue = new SerializableDictionary<string, string>();
        objectDialogue["objectDialogue Key"] = "Hello Value";

    }
}


