using NUnit.Framework;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField]
    private string playerIDString;

    [SerializeField]
    private IDCard currentPlayerID;

    [SerializeField]
    private Dictionary<string, IDCard> playerIDs = new Dictionary<string,IDCard>();

    [SerializeField]
    private Dictionary<string, Interactable> itemInventory;

    private DialogueManager dialogueManager;

    //How should this work?
    //Player info should contain all player info and do things player can do
    //should have player inventory and ability to switch items
    //should have id inventory and ability to id switch
    //after an id switch it should list 

    private void Start()
    {
        itemInventory = new Dictionary<string, Interactable>();
        currentPlayerID = IDCard.None;
        playerIDString = currentPlayerID.Name;
        dialogueManager = DialogueManager.instance;
    }
    public IDCard getPlayerID() {
            return currentPlayerID;
         }
    public bool switchPlayerID(string idName)
    {
        if (!playerIDs.ContainsKey(idName))
        {
            return false;
        }
        currentPlayerID = playerIDs[idName];
        playerIDString = currentPlayerID.Name;
        string idSwitchDialogue = "I feel the fog of confusion around my mind grow denser until... suddenly it clears...I believe my real identity is that of a " + currentPlayerID.Name + ".";
        try
        {
            string switchDialogue = dialogueManager.GetDialogue(idName + " id", "switch", "Any");
            
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogWarning(e);
        }
        GameManager.Instance().AddTextToJournal(idSwitchDialogue);

        return true;
        
    }
    public bool getItemFromInventory(string item)
    {
        if (itemInventory.ContainsKey(item)) return true;
        return false;
    }
    public Dictionary<string, Interactable> getPlayerInventory()
    {
        return itemInventory;
    }
    public void addItem(Interactable item)
    {
        IDCard possibleCard = IDCard.StringAsID(item.interactableName.Substring(0,item.interactableName.Length-3));
        //If item is card add it to the card inventory
        if (!possibleCard.Equals(IDCard.None))
        {
            playerIDs.Add(possibleCard.Name.ToLower(), possibleCard);
        }
        //add item to item inventory regardless
        itemInventory.Add(item.interactableName.ToLower(), item);

    }
    public string listInventory()
    {
        StringBuilder stringBuilder = new StringBuilder();
        int count = 1;
        stringBuilder.Append("ID Cards: \n");
        foreach (IDCard id in playerIDs.Values)
        {
            stringBuilder.Append(count + ". ").Append(id.Name).Append("\n");
            count++;
        }
        count = 1;
        stringBuilder.Append("Other Items: \n");
        foreach (string item in itemInventory.Keys){
            if (IDCard.StringAsID(item).Equals(IDCard.None))
            {
                stringBuilder.Append(count + ". ").Append(item).Append("\n");
                count++;
            }
        }
        if(itemInventory.Keys.Count == 0)
        {
            stringBuilder.Append("None");
        }
        return stringBuilder.ToString();
    }
}
