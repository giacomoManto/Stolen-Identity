using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Handles player-related data including player ID, inventory management,
/// and interactions such as switching identities and listing items.
/// </summary>
public class PlayerInfo : MonoBehaviour
{
    #region Serialized Fields

    // The string representation of the current player's ID.
    [SerializeField]
    private string playerIDString;

    // The current player's ID card.
    [SerializeField]
    private IDCard currentPlayerID;

    // A dictionary mapping id names to corresponding ID cards.
    [SerializeField]
    private Dictionary<string, IDCard> playerIDs = new Dictionary<string, IDCard>();
    private Dictionary<string, Interactable> playerIDInteractables = new Dictionary<string, Interactable>();

    // A dictionary mapping item names to Interactable items.
    [SerializeField]
    private Dictionary<string, Interactable> itemInventory;

    [SerializeField]
    private bool secondId = false;

    #endregion

    #region Private Fields

    // Reference to the DialogueManager instance.
    private DialogueManager dialogueManager;

    #endregion

    #region Unity Methods

    /// <summary>
    /// Unity Start method initializes the inventory, sets the initial player ID,
    /// and grabs the dialogue manager instance.
    /// </summary>
    private void Start()
    {
        // Initialize the item inventory.
        itemInventory = new Dictionary<string, Interactable>();

        // Set default player ID to a "None" value.
        currentPlayerID = IDCard.None;
        playerIDString = currentPlayerID.Name;

        // Initialize the dialogue manager instance.
        dialogueManager = DialogueManager.instance;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the current player's ID card.
    /// </summary>
    /// <returns>The current IDCard.</returns>
    public IDCard getPlayerID()
    {
        return currentPlayerID;
    }

    /// <summary>
    /// Switches the player's ID to the specified idName if available.
    /// Adds a journal entry and optionally triggers a dialogue event.
    /// </summary>
    /// <param name="idName">The name of the ID to switch to.</param>
    /// <returns>True if the switch is successful, false otherwise.</returns>
    public bool switchPlayerID(string idName)
    {
        idName = idName.ToLower().Split()[0];
        Debug.Log("trying to switch to " + idName);

        // Check if the requested ID is already the current one.
        if (idName == currentPlayerID.Name)
        {
            string temp = "I feel my eyes glaze over for a moment before realizing I already am a " + playerIDString + ".";
            GameManager.Instance().AddTextToJournal(temp);
            return true;
        }

        if (idName != IDCard.Doctor.Name.ToLower() && idName != IDCard.Guard.Name.ToLower() && idName != IDCard.Patient.Name.ToLower() && idName != IDCard.Brawler.Name.ToLower() && idName != IDCard.Thief.Name.ToLower())
        {
            GameManager.Instance().AddTextToJournal($"I try to put on {idName}'s id but I realize I both don't have it and made up that id. It doesn't exist.");
            return false;
        }

        // Check if the requested ID exists in the player's collection.
        if (!playerIDs.ContainsKey(idName))
        {
            GameManager.Instance().AddTextToJournal("I reach out to use the " + idName + " id, but I realize in order to use it properly, i need to take it.");
            return false;
        }

        // Update the current ID and corresponding string.
        currentPlayerID = playerIDs[idName];
        playerIDString = idName;

        // Prepare the dialogue text for switching identities.
        string idSwitchDialogue = "I feel the fog of confusion around my mind grow denser until... suddenly it clears...I believe my real identity is that of a " + currentPlayerID.Name + ".";
        try
        {
            idSwitchDialogue = DialogueManager.Instance().GetDialogue(idName, "switch", "Any");
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogWarning(e + "when trying to get dialogue for id switch on " + idName + " id.");
        }

        // Add the ID switch dialogue to the game journal.
        GameManager.Instance().AddTextToJournal(idSwitchDialogue);

        return true;
    }

    /// <summary>
    /// Checks if a given item exists in the player's inventory.
    /// </summary>
    /// <param name="item">The name of the item to check.</param>
    /// <returns>True if the item is in the inventory; otherwise, false.</returns>
    public bool isItemInInventory(string item)
    {
        return itemInventory.ContainsKey(item.ToLower()) || itemInventory.ContainsKey(item);
    }

    /// <summary>
    /// Gets the complete dictionary of the player's item inventory.
    /// </summary>
    /// <returns>A dictionary of item names and their corresponding Interactable objects.</returns>
    public Dictionary<string, Interactable> getPlayerInventory()
    {
        Dictionary<string, Interactable> itemInventoryCopy = new Dictionary<string, Interactable>(itemInventory);
        itemInventoryCopy.AddRange(playerIDInteractables);
        return itemInventoryCopy;
    }

    /// <summary>
    /// Adds an item to the player's inventory. If the item represents an ID card,
    /// it is also added to the player's ID collection.
    /// </summary>
    /// <param name="item">The interactable item to add.</param>
    public void addItem(Interactable item)
    {
        if (itemInventory.ContainsKey(item.name.ToLower()))
        {
            GameManager.Instance().AddTextToJournal("I take out the " + item.interactableName + " from my bag and then...");
            return;
        }
        // Determine if the item represents an ID card.
        IDCard possibleCard = IDCard.StringAsID(item.interactableName.Substring(0, item.interactableName.Length - 3));

        // If the item is a valid ID card, add it to the playerIDs dictionary.
        if (!possibleCard.Equals(IDCard.None))
        {
            playerIDInteractables.Add(item.interactableName.ToLower(), item);
            if (!secondId && !possibleCard.Equals(IDCard.Patient))
            {
                GameManager.Instance().AddTextToJournal("As I begin to pick up the " + item.interactableName + " I realize it calls to me, similar to the patient id. I wonder what would happen if I try to <b>use</b> it. I guess my mind would <b>switch</b> into the mindset of a " + possibleCard + ", hard to tell.");
                secondId = true;
            }
            playerIDs.Add(possibleCard.Name.ToLower(), possibleCard);
            return;
        }

        // Add the item to the item inventory regardless.
        itemInventory.Add(item.interactableName.ToLower(), item);
        Debug.Log($"Succesfully added {item.interactableName} to inventory");
    }


    public void removeItem(Interactable item)
    {
        if (itemInventory.ContainsKey(item.interactableName.ToLower()))
        {
            itemInventory.Remove(item.interactableName.ToLower());
            Debug.Log($"Succesfully removed {item.interactableName} from inventory");
        }
        else
        {
            Debug.Log($"Failed to remove {item.interactableName} from inventory. Item not found.");
        }
    }

    public void removeItem(string item)
    {
        if (itemInventory.ContainsKey(item.ToLower()))
        {
            itemInventory.Remove(item.ToLower());
            Debug.Log($"Succesfully removed {item} from inventory");
        }
        else
        {
            Debug.Log($"Failed to remove {item} from inventory. Item not found.");
        }
    }

    /// <summary>
    /// Creates a formatted string listing all ID cards and other items in the inventory.
    /// </summary>
    /// <returns>A formatted string of the inventory.</returns>
    public string listInventory()
    {
        StringBuilder stringBuilder = new StringBuilder();
        int count = 1;

        // List ID cards.
        stringBuilder.Append("ID Cards: \n");
        foreach (IDCard id in playerIDs.Values)
        {
            stringBuilder.Append(count + ". ").Append(id.Name).Append("\n");
            count++;
        }

        // List other items.
        count = 1;
        stringBuilder.Append("Other Items: \n");
        foreach (string item in itemInventory.Keys)
        {
            // Only list the item if it is not an ID card.

            stringBuilder.Append(count + ". ").Append(item).Append("\n");
            count++;

        }

        // If no other items exist, note that.
        if (count <= 1)
        {
            stringBuilder.Append("None");
        }

        return stringBuilder.ToString();
    }

    #endregion
}
