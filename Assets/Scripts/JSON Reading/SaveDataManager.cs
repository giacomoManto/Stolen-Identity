using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///A singleton class(only place one per scene) that keeps track of save and load data.
/// </summary>

//If a refrence or deeper explination is needed this was based on https://www.youtube.com/watch?v=aUi9aijvpgs&ab_channel=ShapedbyRainStudios.
public class SaveDataManager : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Where the player save data will be saved to")]
    private string fileName = "SaveData";

    private SaveData saveData;  //Keeps track of the player data from save/load. Things like player virus and player energy are stored in this format.

    private FileDataHandler<SaveData> saveFileDataHandler;  //A file handler that will load and save player data to and from a Json format.

    public static SaveDataManager instance { get; private set; } //A static variable to ensure theres only one DataPersistenceManager in the scene.

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Save/Load: Found More than one DataPersistenceManager in the current scene. This may cause errors with saving and loading data.");
        }

        instance = this;
        saveData = new SaveData();
        saveFileDataHandler = new FileDataHandler<SaveData>(Application.persistentDataPath, fileName);

    }
    public static SaveDataManager Instance()
    {
        return instance;
    }

    /// <summary>
    ///Creates a new game by initializing playerData with its default constructor values.
    /// </summary>
    public void CheckForDataObjects()
    {
        if (saveData == null)
        {
            Debug.Log("Save/Load: No player data found on game load, creating new player data.");
            saveData = new SaveData();
        }
        if (saveFileDataHandler.Load() == null)
        {
            Debug.Log("Save/Load: No player data found on game load, creating new player data.");
            saveFileDataHandler.Save(saveData);
        }
    }

    /// <summary>
    /// Loads the save data from file and returns the save data as an object.
    /// </summary>
    /// <param name="save">The save data in the required save data format</param>
    public SaveData LoadGame()
    {
        Debug.Log("Loading Save Data");
        CheckForDataObjects();
        saveData = saveFileDataHandler.Load();

        return saveData;
    }

    /// <summary>
    /// The save data to be saved to file.
    /// </summary>
    /// <param name="save">The save data in the required save data format</param>
    public void SaveGame(SaveData save)
    {
        CheckForDataObjects();
        Debug.Log("Saving Data");
        saveFileDataHandler.Save(save);
    }
}
