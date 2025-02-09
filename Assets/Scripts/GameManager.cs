using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private IDCard currentPlayerID = IDCard.Patient;

    [SerializeField]
    private TMP_InputField playerInput;

    [SerializeField]
    private RoomBehavior currentPlayerRoom;

    [SerializeField]
    private Dictionary<string, RoomBehavior> allRooms = new Dictionary<string, RoomBehavior>();

    private JournalOutput journal;

    private DialogueManager dialogueManager;

    private static GameManager instance;

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

        //Grab all rooms
        IEnumerable<RoomBehavior> rooms = FindObjectsByType<RoomBehavior>(FindObjectsSortMode.None);
        List<RoomBehavior> temp = new List<RoomBehavior>(rooms);
        foreach (RoomBehavior room in temp)
        {
            allRooms[room.name] = room;
        }
        //Set current player room to starting room, this should be changed on load
        currentPlayerRoom = allRooms["Starting Room"];
        journal.AddGameText(currentPlayerRoom.GetRoomDescription(currentPlayerID));

        dialogueManager = FindFirstObjectByType<DialogueManager>();

    }


    public void handlePlayerInput(string playerInput)
    {
        print(playerInput);
        string currentPlayerInput = playerInput.ToLower();
        //Check for valid input (noun and verb)
        if (currentPlayerInput.Split(" ").Length < 2)
        {
            journal.AddGameText("You pause, realizing you probably need a noun and a verb to act properly. ");
            print("Invalid input. Please enter a verb and a noun.");
            return;
        }

        //Add the action to the journal while passing the action to the room
        journal.AddGameText(currentPlayerRoom.ActOn(currentPlayerID, currentPlayerInput));
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        journal.Clear();
        journal.AddGameText(this.currentPlayerRoom.GetRoomDescription(currentPlayerID));
    }
    public String getDialogueOrDefault(string dialogueKey)
    {
        return dialogueManager.getDialogue(dialogueKey);
    }
    }
