using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerInput;

    [SerializeField]
    private RoomBehavior currentPlayerRoom;

    [SerializeField]
    private Dictionary<string, RoomBehavior> allRooms = new Dictionary<string, RoomBehavior>();

    private PlayerInfo player;
    private JournalOutput journal;
    private static GameManager instance;
    private Dictionary<IDCard, int> statRecorder = new Dictionary<IDCard, int>();
    private Dictionary<string, CommandTemplate> genericCommands = new Dictionary<string, CommandTemplate>();


    private Dictionary<string, bool> gameFlags = new Dictionary<string, bool>();

    #region Singleton Implementation
    public static GameManager Instance()
    {
        return instance;
    }

    GameManager()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    #endregion

    #region Initialization
    void Start()
    {
        // Now it's safe to initialize other components.
        journal = JournalOutput.Instance();
        player = FindFirstObjectByType<PlayerInfo>();

        IEnumerable<RoomBehavior> rooms = FindObjectsByType<RoomBehavior>(FindObjectsSortMode.None);
        List<RoomBehavior> temp = new List<RoomBehavior>(rooms);
        foreach (RoomBehavior room in temp)
        {
            allRooms[room.name] = room;
        }

        currentPlayerRoom = allRooms["Starting Room"];

        addGenericCommand(new CheckBag(this, player));
        addGenericCommand(new CheckInventory(this, player));
        addGenericCommand(new InspectCommand(this));
        addGenericCommand(new SaveGame(this, player));
        addGenericCommand(new UseCommand(this));
        addGenericCommand(new ViewRoom(this, player));
        addGenericCommand(new InfoCommand(this));

        // Start coroutine to wait for DialogueManager to load before initializing dialogue elements.
        StartCoroutine(WaitForDialogueAndInit());
    }

    private IEnumerator WaitForDialogueAndInit()
    {
        // Wait until DialogueManager exists and has finished loading.
        yield return new WaitUntil(() =>
            DialogueManager.instance != null && DialogueManager.instance.IsDialogueLoaded);

        errorCheckStart();
        displayCurrentRoomDesc();
    }
    #endregion

    #region GameState Management
    public void SetFlag(string key, bool value)
    {
        if (gameFlags.ContainsKey(key))
        {
            gameFlags[key] = value;
            Debug.Log("Updated " + key + " to " + value);
        }
        else
        {
            gameFlags.Add(key, value);
            Debug.Log("Added " + key + " with value " + value);
        }
    }

    public bool GetFlag(string key, bool defaultFlag = false)
    {
        if (gameFlags.ContainsKey(key))
        {
            return gameFlags[key];
        }
        else
        {
            Debug.LogError("Key " + key + " not found in gameFlags");
            return defaultFlag;
        }
    }
    #endregion

    #region Player Input Handling
    public void handlePlayerInput(string playerInput)
    {
        statRecorder[player.getPlayerID()] = (statRecorder.TryGetValue(player.getPlayerID(), out int val) ? val : 0) + 1;
        Debug.Log(playerInput);
        string currentPlayerInput = playerInput.ToLower();

        if (currentPlayerInput.Split(" ").Length < 2)
        {
            AddTextToJournal("You pause, realizing you probably need a noun and a verb to act properly.");
            Debug.Log("Invalid input. Please enter a verb and a noun.");
            return;
        }

        if (checkBuiltInCommands(currentPlayerInput)) return;
        ActOnRoomObjects(player.getPlayerID(), currentPlayerInput);
    }

    public void ActOnRoomObjects(IDCard currentIDCard, string playerInput)
    {
        Dictionary<string, Interactable> ItemDictionary = currentPlayerRoom.getItemDictionary();
        if (string.IsNullOrEmpty(playerInput))
        {
            Debug.LogError("Empty player input");
        }
        string Action = "";
        string Object = "";
        foreach (string item in ItemDictionary.Keys)
        {
            if (playerInput.Contains(item))
            {
                Object = item;
                Action = playerInput.Replace(item, "");
                Action = Action.Substring(0, Action.Length - 1);
            }
        }
        if (string.IsNullOrEmpty(Object))
        {
            Object = playerInput.Split(" ")[1];
            Debug.LogWarning("Attempted to act on " + Object + " but it was not found in the item list of " + currentPlayerRoom.name);
            AddTextToJournal("I must be losing my sanity... there is no " + Object + " in this room...");
        }
        else
        {
            ItemDictionary[Object].PerformAction(Action, currentIDCard);
        }
    }
    #endregion

    #region Inventory Management
    public void addObjectToPlayerInventory(Interactable item)
    {
        item.transform.parent = player.transform;
        player.addItem(item);
        currentPlayerRoom.InitIteractables();
    }
    #endregion

    #region Command Handling
    public bool checkBuiltInCommands(string playerInput)
    {
        if (checkListActions(playerInput))
        {
            return true;
        }
        if (playerInput.StartsWith("switch"))
        {
            if (player.switchPlayerID(playerInput.Split(" ")[1]))
            {
                return true;
            }
        }

        if (playerInput.StartsWith("info"))
        {
            foreach (string command in genericCommands.Keys)
            {
                if (playerInput.Contains(command))
                {
                    AddTextToJournal(genericCommands[command].getDescription());
                    return true;
                }
            }
        }

        if (genericCommands.ContainsKey(playerInput))
        {
            Debug.Log("Now running command: " + playerInput);
            return genericCommands[playerInput].Execute();
        }

        return false;
    }

    private void addGenericCommand(CommandTemplate command)
    {
        genericCommands.Add(command.getName(), command);
    }

    private bool checkListActions(string playerInput)
    {
        if (playerInput == "list actions" || playerInput == "check actions" || playerInput == "list commands")
        {
            string actionsList = "";
            foreach (string command in genericCommands.Keys)
            {
                actionsList += command + "\n";
            }
            actionsList = actionsList.Substring(0, actionsList.Length - 1);
            AddTextToJournal(actionsList);
            return true;
        }
        return false;
    }
    #endregion

    #region Room Management
    public void displayCurrentRoomDesc()
    {
        this.currentPlayerRoom.DisplayRoomDescription(player.getPlayerID());
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        displayCurrentRoomDesc();
    }
    #endregion

    #region Journal Management
    public void AddTextToJournal(string text)
    {
        journal.AddGameText(text);
    }
    #endregion

    #region Error Checking
    private void errorCheckStart()
    {
        if (currentPlayerRoom == null)
            Debug.LogError("currentPlayerRoom is null");
        if (allRooms == null || allRooms.Keys.Count <= 0)
            Debug.LogError("allRooms is null or was not populated");
        if (player == null)
            Debug.LogError("player is null or was not populated");
        if (journal == null)
            Debug.LogError("journal is null or was not populated");
        if (statRecorder == null)
            Debug.LogError("statRecorder is null or was not populated");
        if (genericCommands == null)
            Debug.LogError("genericCommands is null or was not populated");
    }
    #endregion
}
