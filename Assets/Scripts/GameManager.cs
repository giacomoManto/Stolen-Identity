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
    TMP_InputField playerInput;

    [SerializeField]
    RoomBehavior currentPlayerRoom;

    [SerializeField]
    IDCard currentPlayerID = IDCard.Patient;

    [SerializeField]
    Dictionary<string, RoomBehavior> allRooms = new Dictionary<string, RoomBehavior>();

    private JournalOutput journal;

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

    }

    private void Update()
    {
        // Set Room Description Here TODO IN FUTURE ONLY GET ROOM DESCRIPTION WHEN NEEDED.
        // THIS IS NOT CORRECT TEMPORARY. We should print when we enter a new room and when the player requests it.
        //if (roomDescription.text != currentPlayerRoom.GetRoomDescription(currentPlayerID))
        //{
        //    roomDescription.text = currentPlayerRoom.GetRoomDescription(currentPlayerID);
        //}
    }

    public void readPlayerInput()
    {
        print(playerInput.text);
        journal.AddPlayerText(playerInput.text);
        string[] split = playerInput.text.Split(" ");
        if (split.Length != 2)
        {
            print("Invalid input. Please enter a verb and a noun.");
            return;
        }
        journal.AddGameText(currentPlayerRoom.ActOn(currentPlayerID, split[1], split[0]));
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        journal.Clear();
        journal.AddGameText(this.currentPlayerRoom.GetRoomDescription(currentPlayerID));
    }

    //void OnGUI()
    //{
    //    Event e = Event.current;
    //    if (e.isKey)
    //    {
    //        Debug.Log("Detected key code: " + e.keyCode);
    //    }
    //}
}
