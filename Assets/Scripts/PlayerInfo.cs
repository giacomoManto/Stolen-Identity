using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private string playerIDString;

    [SerializeField]
    private IDCard currentPlayerID;

    [SerializeField]
    private List<IDCard> playerIDs = new List<IDCard>();

    [SerializeField]
    private Dictionary<string, Interactable> itemInventory;

    private void Start()
    {
        itemInventory = new Dictionary<string, Interactable>();
        currentPlayerID = IDCard.Patient;
        playerIDs.Add(currentPlayerID);
        playerIDString = currentPlayerID.Name;
    }
    public IDCard getPlayerID() {
        
            return currentPlayerID;
         }
    public bool switchPlayerID(IDCard id)
    {
        if (playerIDs.Contains(id))
        {
            currentPlayerID = id;
            playerIDString = currentPlayerID.Name;
            return true;
        }
        return false;
    }
    public bool getInventory(string item)
    {
        if (itemInventory.ContainsKey(item)) return true;
        return false;
    }
    public void addItem(Interactable item)
    {
        itemInventory.Add(item.name, item);
    }
}
