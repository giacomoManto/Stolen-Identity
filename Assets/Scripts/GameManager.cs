using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField playerInput;

    [SerializeField]
    TMP_Text roomDescription;

    [SerializeField]
    RoomBehavior currentPlayerRoom;

    [SerializeField]
    IDCard currentPlayerID = IDCard.Patient;
   

    [SerializeField]
    Dictionary<string, RoomBehavior> allRooms = new Dictionary<string, RoomBehavior>();
    void Start()
    {
        //Grab all rooms
        IEnumerable<RoomBehavior> rooms = FindObjectsOfType<MonoBehaviour>().OfType<RoomBehavior>();
        List<RoomBehavior>temp = new List<RoomBehavior>(rooms);
        foreach (RoomBehavior room in temp)
        {
            allRooms[room.name] = room; 
        }
        //Set current player room to starting room, this should be changed on load
        currentPlayerRoom = allRooms["Starting Room"];
        //Set room description
        roomDescription.text = currentPlayerRoom.GetRoomDescription(currentPlayerID);
    }

    public void readPlayerInput()
    {
        print(playerInput.text);
    }

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey)
        {
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }
}
