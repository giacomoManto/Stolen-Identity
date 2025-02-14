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

    public static GameManager GetInstance()
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

    void Start()
    {
        // Now it's safe to initialize other components.
        journal = JournalOutput.GetInstance();
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

    public void handlePlayerInput(string playerInput)
    {
        statRecorder[player.getPlayerID()] = (statRecorder.TryGetValue(player.getPlayerID(), out int val) ? val : 0) + 1;
        Debug.Log(playerInput);
        string currentPlayerInput = playerInput.ToLower();

        if (currentPlayerInput.Split(" ").Length < 2)
        {
            journal.AddGameText("You pause, realizing you probably need a noun and a verb to act properly.");
            Debug.Log("Invalid input. Please enter a verb and a noun.");
            return;
        }

        if (checkBuiltInCommands(currentPlayerInput)) return;
        journal.AddGameText(ActOnRoomObjects(player.getPlayerID(), currentPlayerInput));
    }

    public string ActOnRoomObjects(IDCard currentIDCard, string playerInput)
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
            return "I must be losing my sanity... there is no " + Object + " in this room...";
        }
        return ItemDictionary[Object].PerformAction(Action, currentIDCard);
    }

    public bool checkBuiltInCommands(string playerInput)
    {
        if (checkListActions(playerInput))
        {
            return true;
        }
        if (playerInput.StartsWith("switch"))
        {
            if(player.switchPlayerID(playerInput.Split(" ")[1]))
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
                    addTextToJournal(genericCommands[command].getDescription());
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
            addTextToJournal(actionsList);
            return true;
        }
        return false;
    }

    public void displayCurrentRoomDesc()
    {
        addTextToJournal(this.currentPlayerRoom.GetRoomDescription(player.getPlayerID()));
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        journal.Clear();
        displayCurrentRoomDesc();
    }

    public void addTextToJournal(string text)
    {
        journal.AddGameText(text);
    }

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
}
