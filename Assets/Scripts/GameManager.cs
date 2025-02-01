using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField playerInput;

    [SerializeField]
    RoomBehavior currentPlayerRoom;

    [SerializeField]
    List<RoomBehavior> allRooms = new List<RoomBehavior>();
    void Start()
    {
        
    }

    private void readPlayerInput()
    {
        print(playerInput.text);
    }
}
