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


    public void handlePlayerInput(string playerInput)
    {
        playerInput = playerInput.ToLower();
        journal.AddGameText(currentPlayerRoom.ActOn(currentPlayerID, playerInput));
    }

    public void changeRoom(RoomBehavior room)
    {
        this.currentPlayerRoom = room;
        journal.Clear();
        journal.AddGameText(this.currentPlayerRoom.GetRoomDescription(currentPlayerID));
    }


}
