using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class RoomBehavior : MonoBehaviour
{
    [Header("Room Settings")]
    public int securityLevel = 0;
    public String roomName = "";
    [SerializeField]
    String roomDescription = "";
    [SerializeField]
    List<string> consideredInRoom = new List<string>();

    [Header("Visit Text")]
    [SerializeField]
    private List<string> visitText;
    private int timesVisited = 0;
    [SerializeField]
    private string allAfter = string.Empty;


    private Dictionary<string, Interactable> ItemDictionary = new Dictionary<string, Interactable>();

    private void Awake()
    {
        InitInteractables();
    }

    public void InitInteractables()
    {
        ItemDictionary.Clear();
        consideredInRoom.Clear();
        Interactable[] childrenInteractables = GetComponentsInChildren<Interactable>();
        foreach(Interactable child in childrenInteractables)
        {
            if(!child.destroyed)
            {
                Debug.Log(child.interactableName);
                ItemDictionary.Add(child.interactableName.ToLower(), child);
                consideredInRoom.Add(child.interactableName.ToLower());
            }
            
        }
    }

    public void OnEnter()
    {
        if (timesVisited < visitText.Count)
        {
            GameManager.Instance().AddTextToJournal(visitText[timesVisited]);
            timesVisited++;
        }
        else if (allAfter.Length > 0)
        {
            GameManager.Instance().AddTextToJournal(allAfter);
        }
    }

    /// <summary>
    /// Gets the room description based on the player ID currently held.
    /// </summary>
    /// <param name="currentIDCard">The Player ID name in all lower case.</param>
    /// <returns>A room description.</returns>
    public void DisplayRoomDescription(IDCard currentIDCard)
    {
        StringBuilder fullRoomDescription = new StringBuilder();
        fullRoomDescription.Append(roomDescription);

        //For each item in the list, print the specific room description from the player
        foreach (Interactable item in ItemDictionary.Values)
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
        GameManager.Instance().AddTextToJournal(fullRoomDescriptionString);
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
    public Dictionary<string,Interactable> getItemDictionary()
    {
        return ItemDictionary;
    }
    public void removeItemFromDictionary(Interactable item)
    {
        if (ItemDictionary.ContainsKey(item.interactableName.ToLower()))
        {
            Debug.Log("removed " + item.interactableName);
            ItemDictionary.Remove(item.interactableName.ToLower());

        }

    }
}
