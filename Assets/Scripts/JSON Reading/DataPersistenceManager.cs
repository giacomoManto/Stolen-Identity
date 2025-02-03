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
public class DataPersistenceManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Where the dialogue will be pulled from")]
    private string fileName;

    private DialogueTemplate dialogueData;  //Keeps track of the player data from save/load. Things like player virus and player energy are stored in this format.

    private FileDataHandler<DialogueTemplate> dataHandler;  //A file handler that will load and save player data to and from a Json format.


    public static DataPersistenceManager instance { get; private set; } //A static variable to ensure theres only one DataPersistenceManager in the scene.

    private void Awake()
    {
          instance = this;
        string path = Application.dataPath +  "\\Dialogue";
        dataHandler = new FileDataHandler<DialogueTemplate>(path, fileName);
        Debug.Log(path);
    }
    private void Start()
    {
        dialogueData = new DialogueTemplate();
        dataHandler.Save(dialogueData);

    }

}



