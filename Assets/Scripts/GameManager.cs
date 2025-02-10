using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
        journal = JournalOutput.GetInstance();

        player = FindFirstObjectByType<PlayerInfo>();

        //Grab all rooms
        IEnumerable<RoomBehavior> rooms = FindObjectsByType<RoomBehavior>(FindObjectsSortMode.None);
        List<RoomBehavior> temp = new List<RoomBehavior>(rooms);
        foreach (RoomBehavior room in temp)
        {
            allRooms[room.name] = room;
        }
        //Set current player room to starting room, this should be changed on load
        currentPlayerRoom = allRooms["Starting Room"];
        journal.AddGameText(currentPlayerRoom.GetRoomDescription(player.getPlayerID()));
    }


    public void handlePlayerInput(string playerInput)
    {
        statRecorder[player.getPlayerID()] = (statRecorder.TryGetValue(player.getPlayerID(), out int val) ? val : 0) + 1;   
        print(playerInput);
        string currentPlayerInput = playerInput.ToLower();
        //Check for valid input (noun and verb)
        if (currentPlayerInput.Split(" ").Length < 2)
        {
            journal.AddGameText("You pause, realizing you probably need a noun and a verb to act properly. ");
            print("Invalid input. Please enter a verb and a noun.");
            return;
        }
        if (checkBuiltInCommands(playerInput)) return; 
        //Add the action to the journal while passing the action to the room
        journal.AddGameText(currentPlayerRoom.ActOn(player.getPlayerID(), currentPlayerInput));
    }

    public bool checkBuiltInCommands(string playerInput)
    {
        switch (playerInput)
        {
            case "check bag":
                journal.AddGameText(player.listInventory());
                return true;
            case "list inventory":
                journal.AddGameText(player.listInventory());
                return true;
            case "main menu":
                journal.AddGameText("Implement main menu");
                return true;
            case "save game":
                journal.AddGameText("Implement save game");
                return true;
            default:
                return false;
        }
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        journal.Clear();
        journal.AddGameText(this.currentPlayerRoom.GetRoomDescription(player.getPlayerID()));
    }
    }
