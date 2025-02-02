using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class RoomBehavior : MonoBehaviour
{
    public int securityLevel = 0;
    public String roomName = "";
    public List<RoomBehavior> hasDoorsTo= new List<RoomBehavior>();
    [SerializeField]
    String roomDescription = "";
    [SerializeField]
    Dictionary<String,String> ItemList = new Dictionary<String, String>(); //Temporary until we have an object class


    /// <summary>
    /// Gets the room description based on the player ID currently held.
    /// </summary>
    /// <param name="currentIDCard">The Player ID name in all lower case.</param>
    /// <returns>A room description.</returns>
    public String GetRoomDescription(IDCard currentIDCard)
    {
        StringBuilder fullRoomDescription = new StringBuilder();
        fullRoomDescription.Append(roomDescription);

        //For each item in the list, print the specific room description from the player
        foreach (var item in ItemList)
        {
            String descriptionIndex = currentIDCard.Name + item.Key;

            ///REPLACE THIS
            String itemRoomDescription = item.Value;    //Go to item, get description based on description index.
            ///REPLACE THIS
            
            fullRoomDescription.AppendLine(itemRoomDescription);
        }
        fullRoomDescription.AppendLine("\nThe room also seems to have doors to ");
        foreach (var room in hasDoorsTo)
        {
            fullRoomDescription.AppendLine( "the " + room.roomName + ", ");
        }
            return fullRoomDescription.ToString();
    }
    /// <summary>
    /// Performs a player action on an object in the room. The action performed may be based on the current ID card.
    /// </summary>
    /// <param name="Object"></param>
    /// <param name="Action"></param>
    public void ActOn(String currentIDCard, String Object, String Action)
    {
        if (!ItemList.ContainsKey(Object))
        {
            Debug.LogError("Attempted to act on " + Object + " but it was not found in the item list of " + roomName);
            return;
        }
        //ItemList[Object].PerformAction(currentIDCard, Action);
    }

    public float getRoomCatchChance(float globalCatchChance, IDCard currentIDCard)
    {
        float catchChance = globalCatchChance + 0.1f*(securityLevel - currentIDCard.SecurityLevel);
        if (currentIDCard.Name == "Theif")
        {
            catchChance = catchChance * .5f;
        }
        return Math.Clamp(catchChance,0f,1f);
        
    }

    

}
