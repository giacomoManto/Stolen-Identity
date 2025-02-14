using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField playerInput; // UI input field for player commands

    [SerializeField]
    private RoomBehavior currentPlayerRoom; // Tracks the player's current room

    [SerializeField]
    private Dictionary<string, RoomBehavior> allRooms = new Dictionary<string, RoomBehavior>(); // Stores all rooms by name

    private PlayerInfo player; // Reference to the player

    private JournalOutput journal; // Reference to the in-game journal for text output

    private static GameManager instance; // Singleton instance of GameManager

    private Dictionary<IDCard, int> statRecorder = new Dictionary<IDCard, int>(); // Tracks player actions using their ID

    private Dictionary<string, CommandTemplate> genericCommands = new Dictionary<string, CommandTemplate>(); // Stores available commands

    public static GameManager GetInstance()
    {
        return instance;
    }

    GameManager()
    {
        // Ensure only one instance of GameManager exists (Singleton pattern)
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
        journal = JournalOutput.GetInstance();
        player = FindFirstObjectByType<PlayerInfo>();

        // Gather all RoomBehavior objects in the scene and store them in allRooms dictionary
        IEnumerable<RoomBehavior> rooms = FindObjectsByType<RoomBehavior>(FindObjectsSortMode.None);
        List<RoomBehavior> temp = new List<RoomBehavior>(rooms);
        foreach (RoomBehavior room in temp)
        {
            allRooms[room.name] = room;
        }

        // Set the starting room (should be changed on game load)
        currentPlayerRoom = allRooms["Starting Room"];
        displayCurrentRoomDesc();

        // Register built-in commands
        addGenericCommand(new CheckBag(this, player));
        addGenericCommand(new CheckInventory(this, player));
        addGenericCommand(new InspectCommand(this));
        addGenericCommand(new SaveGame(this, player));
        addGenericCommand(new UseCommand(this));
        addGenericCommand(new ViewRoom(this, player));

    }

    public void handlePlayerInput(string playerInput)
    {
        // Record the number of actions a player has taken
        statRecorder[player.getPlayerID()] = (statRecorder.TryGetValue(player.getPlayerID(), out int val) ? val : 0) + 1;

        print(playerInput);
        string currentPlayerInput = playerInput.ToLower();

        // Ensure input contains both a verb and a noun
        if (currentPlayerInput.Split(" ").Length < 2)
        {
            journal.AddGameText("You pause, realizing you probably need a noun and a verb to act properly.");
            print("Invalid input. Please enter a verb and a noun.");
            return;
        }

        // Check if input matches built-in commands
        if (checkBuiltInCommands(currentPlayerInput)) return;

        // Log action to journal and pass it to the current room
        journal.AddGameText(ActOnRoomObjects(player.getPlayerID(), currentPlayerInput));
    }
    /// <summary>
    /// Performs a player action on an object in the room. The action performed may be based on the current ID card.
    /// </summary>
    /// <param name="Object"></param>
    /// <param name="Action"></param>
    public string ActOnRoomObjects(IDCard currentIDCard, String playerInput)
    {
        Dictionary<string, Interactable> ItemDictionary = currentPlayerRoom.getItemDictionary();
        if (playerInput == "" || playerInput == null)
        {
            // Our earlier systems should not send empty player input to this function.
            Debug.LogError("Empty player input");
        }
        String Action = "";
        String Object = "";
        foreach (String item in ItemDictionary.Keys)
        {
            if (playerInput.Contains(item))
            {
                Object = item;
                Action = playerInput.Replace(item, "");
                Action = Action.Substring(0, Action.Length - 1);
            }

        }
        if (Object.Equals(""))
        {
            Object = playerInput.Split(" ")[1];
            Debug.LogWarning("Attempted to act on " + Object + " but it was not found in the item list of " + currentPlayerRoom.name);
            return "I must be losing my sanity... there is no " + Object + " in this room...";
        }
        return ItemDictionary[Object].PerformAction(Action, currentIDCard);
    }
    /*
     * ======================================================================
     * Built-in Command Handling
     * ======================================================================
     */

    public bool checkBuiltInCommands(string playerInput)
    {
        // Check if input is a list command
        if (checkListActions(playerInput))
        {
            return true;
        }

        // Check if input is an "info" command to describe available actions
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

        // Execute a recognized command
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
        // Handle different ways of listing available commands
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

    /*
     * ======================================================================
     * Room Management and Journal Updates
     * ======================================================================
     */

    public void displayCurrentRoomDesc()
    {
        addTextToJournal(this.currentPlayerRoom.GetRoomDescription(player.getPlayerID()));
    }

    public void changeRoom(RoomBehavior room)
    {
        // Updates the player's current room and refreshes the journal
        this.currentPlayerRoom = room;
        journal.Clear();
        displayCurrentRoomDesc();
    }

    public void addTextToJournal(string text)
    {
        journal.AddGameText(text);
    }
}
