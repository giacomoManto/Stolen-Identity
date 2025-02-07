using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class RoomBehavior : MonoBehaviour
{
    public int securityLevel = 0;
    public String roomName = "";
    [SerializeField]
    String roomDescription = "";
    [SerializeField]
    List<Interactable> ItemList = new List<Interactable>();


    private Dictionary<string, Interactable> ItemDictionary = new Dictionary<string, Interactable>();

    private void Awake()
    {
        ItemList = new List<Interactable>(GetComponentsInChildren<Interactable>());
        foreach (Interactable item in ItemList)
        {
            ItemDictionary.Add(item.interactableName.ToLower(), item);
        }
    }

    /// <summary>
    /// Gets the room description based on the player ID currently held.
    /// </summary>
    /// <param name="currentIDCard">The Player ID name in all lower case.</param>
    /// <returns>A room description.</returns>
    public string GetRoomDescription(IDCard currentIDCard)
    {
        StringBuilder fullRoomDescription = new StringBuilder();
        fullRoomDescription.Append(roomDescription);

        //For each item in the list, print the specific room description from the player
        foreach (Interactable item in ItemList)
        {
            fullRoomDescription.Append(" ");
            fullRoomDescription.Append(item.GetDescription(currentIDCard));
        }

        //Bold the item names in the room 
        string fullRoomDescriptionString = fullRoomDescription.ToString();

        foreach (string item in ItemDictionary.Keys)
        {
            fullRoomDescriptionString = Regex.Replace(fullRoomDescriptionString, item, $"<b>{item}</b>", RegexOptions.IgnoreCase);
        }
        return fullRoomDescriptionString;
    }

    /// <summary>
    /// Performs a player action on an object in the room. The action performed may be based on the current ID card.
    /// </summary>
    /// <param name="Object"></param>
    /// <param name="Action"></param>
    public string ActOn(IDCard currentIDCard, String playerInput)
    {
        String Action = "";
        String Object = "";
        foreach (String item in ItemDictionary.Keys)
        {
            if (playerInput.Contains(item))
            {
                Object = item;
                Action = playerInput.Replace(item,"");
                Action = Action.Substring(0, Action.Length-1);
            }

        }
        if (Action.Equals("") && Object.Equals(""))
        {
            Debug.LogWarning("Empty player input");
            return $"Your mind goes blank for a bit before you realize you should probably write something in the journal.";
        }
        else if (Object.Equals(""))
        {
            Debug.LogWarning("Attempted to act on " + Object + " but it was not found in the item list of " + roomName);
            return $"I seem to be hallucinating. There is no {char.ToUpper(Object[0]) + Object.Substring(1)} in this room.";
        }
        return ItemDictionary[Object].PerformAction(Action, currentIDCard);
    }

    public float getRoomCatchChance(float globalCatchChance, IDCard currentIDCard)
    {
        float catchChance = globalCatchChance + 0.1f * (securityLevel - currentIDCard.SecurityLevel);
        if (currentIDCard.Equals(IDCard.Thief))
        {
            catchChance = catchChance * .5f;
        }
        return Math.Clamp(catchChance, 0f, 1f);

    }
}
