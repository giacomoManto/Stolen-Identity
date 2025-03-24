using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string interactableName;

    private Dictionary<string, Action<IDCard>> actions = new Dictionary<string, Action<IDCard>>();

    [SerializeField]
    protected string locationDescription;

    [SerializeField]
    protected bool playerCanStore = true;

    [SerializeField]
    private DialogueManager dialogueManager;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Interactable()
    {
        this.RegisterAction(InspectObject, "inspect", "look at", "peek at", "stare at");
        this.RegisterAction(TakeObject, "grab", "take", "steal", "pick up", "collect", "retrieve", "gather", "acquire", "obtain", "procure", "snatch", "seize", "hoard", "yoink");
    }
    void Awake()
    {
        dialogueManager = FindFirstObjectByType<DialogueManager>();
    }
    protected void SetName(string name)
    {
        this.interactableName = name;
    }

    /// <summary>
    /// Registers an action to be performed on this interactable.
    /// </summary>
    /// <param name="actionName"></param>
    /// <param name="action"></param>
    protected void RegisterAction(string actionName, Action<IDCard> action)
    {
        actionName = actionName.ToLower();
        if (!actions.ContainsKey(actionName))
        {
            actions.Add(actionName, action);
        }
        else
        {
            Debug.LogWarning($"Action {actionName} already exists for {interactableName}");
        }
    }

    protected void RegisterAction(Action<IDCard> action, params string[] actionNames)
    {
        foreach (string name in actionNames)
        {
            this.RegisterAction(name, action);
        }
    }

    /// <summary>
    /// Calls the action on the interactable if available.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public void PerformAction(string action, IDCard id)
    {
        action = action.ToLower();
        //Check if action exists in specific action list
        if (actions.ContainsKey(action))
        {
            actions[action](id);
        }
        else
        { 
            try
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson(action, id));
            }
            catch (KeyNotFoundException)
            {

                GameManager.Instance().AddTextToJournal($"Now why would I want to {action} the {interactableName}.");
            }
        }
    }

    //object name + action + ID,  Description
    //object name + action + ID.NONE,  Description

    //appleinspectbrawler, an apple...not as good as booze but its a nice snack
    //applegrab, you grab the apple
    protected string GetTextFromJson(String Action, IDCard id)
    {
        try
        {
            if (dialogueManager == null)
            {
                dialogueManager = FindFirstObjectByType<DialogueManager>();
            }
            return dialogueManager.GetDialogue(this.interactableName, Action, id.Name);
        }
        catch (KeyNotFoundException)
        {
            return $"I try to {Action} the {interactableName} but nothing happens.";
        }
    }

    public virtual void InspectObject(IDCard id)
    {
        if (GetTextFromJson("inspect", id) != $"I try to inspect the {interactableName} but nothing happens.")
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("inspect", id));
        }
        else
        {
            GameManager.Instance().AddTextToJournal("I take a closer look at the " + interactableName + " but I realize there isnt much else to see." );
        }
    }

    /// <summary>
    /// Gets the description of the interactable based on the IDCard. Needs to be overriden.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual string GetDescription(IDCard id)
    {
        if (GetTextFromJson("location", id) != $"I try to location the {interactableName} but nothing happens.")
        {
            return GetTextFromJson("location", id);
        }
        else if (GetTextFromJson("location", IDCard.None) != $"I try to location the {interactableName} but nothing happens.")
        {
            return GetTextFromJson("location", IDCard.None);
        }
        else
        {
            return locationDescription;
        }
    }
    public virtual void TakeObject(IDCard id)
    {
        if (!playerCanStore)
        {
            GameManager.Instance().AddTextToJournal("Pretty sure I dont want to take that " + interactableName);
        }
        else { 
            FindFirstObjectByType<GameManager>().addObjectToPlayerInventory(this);
            GameManager.Instance().AddTextToJournal( "I pocket the " + interactableName + ", putting it into my bag for later use.");
        }
    }
}