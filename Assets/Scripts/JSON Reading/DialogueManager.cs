using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///A singleton class(only place one per scene) that keeps track of save and load data.
///
/// </summary>

//If a reference or deeper explination is needed this was based on https://www.youtube.com/watch?v=aUi9aijvpgs&ab_channel=ShapedbyRainStudios.
public class DialogueManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Where the dialogue will be pulled from")]
    private string fileName;

    private string path = Application.dataPath + "/Dialogue";
    private DialogueTemplate dialogueData;  //Keeps track of the player data from save/load. Things like player virus and player energy are stored in this format.

    private FileDataHandler<SerializableDictionary<String, SerializableDictionary<String, String>>> dataHandler;  //A file handler that will load and save player data to and from a Json format.


    public static DialogueManager instance { get; private set; } //A static variable to ensure theres only one DataPersistenceManager in the scene.

    private void Awake()
    {

        instance = this;
        dataHandler = new FileDataHandler<SerializableDictionary<String, SerializableDictionary<String, String>>>(path, fileName);
        Debug.Log(path);
    }

    private void Start()
    {
        SerializableDictionary<String, SerializableDictionary<String, String>> temp = new SerializableDictionary<String, SerializableDictionary<String, String>>();
        SerializableDictionary<String, String> action = new SerializableDictionary<string, string>();
        action.Add("eat", "you ate the apple.");
        temp.Add("apple", action);
        dataHandler.Save(temp);
        //dialogueData = dataHandler.Load();
        print(dialogueData);
    }

    public String getDialogue(String key)
    {
        //if (dialogueData.roomDialogue.ContainsKey(key))
        //{
        //    return dialogueData.roomDialogue[key];
        //}
        //else if (dialogueData.npcDialogue.ContainsKey(key))
        //{
        //    return dialogueData.npcDialogue[key];
        //}
        //else if (dialogueData.objectDialogue.ContainsKey(key))
        //{
        //    return dialogueData.objectDialogue[key];
        //}
        //else
        //{
        //    return "key not found";
        //}
        return "";
        
    }

}



