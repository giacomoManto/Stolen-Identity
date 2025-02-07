using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
/// <summary>
///Formats the game data into a readable format for the save and load system.
/// </summary>
public class DialogueTemplate
{
    public SerializableDictionary<string, string> roomDialogue; 
    public SerializableDictionary<string, string> objectDialogue;
    public SerializableDictionary<string, string> npcDialogue;
    /**
     * When a new game is created, all values in the constructor should be read into the game
     * This will give the player the below starting values.
     */
    //    1 JSON file
    //object itself will tell JSON what to search up.it will be either:
    //object name + action + ID,  Description
    //object name + action,  Description
    //EXAMPLE:
    //appleinspectbrawler, an apple...not as good as booze but its a nice snack
    //applegrab, you grab the apple
    public DialogueTemplate()
    {
        roomDialogue = new SerializableDictionary<string, string>();
        objectDialogue = new SerializableDictionary<string, string>();
        npcDialogue = new SerializableDictionary<string, string>();
    }
}


