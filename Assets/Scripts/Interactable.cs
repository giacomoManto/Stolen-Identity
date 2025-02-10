using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactableName;

    private Dictionary<string, Func<IDCard, string>> actions = new Dictionary<string, Func<IDCard, string>>();

    [SerializeField]
    protected string location;

    [SerializeField]
    private DialogueManager dialogueManager;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Interactable()
    {
        this.RegisterAction("inspect", GetDescription);
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
    protected void RegisterAction(string actionName, Func<IDCard, string> action)
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

    /// <summary>
    /// Calls the action on the interactable if available.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string PerformAction(string action, IDCard id)
    {
        action = action.ToLower();
        //Check if action exists in specific action list
        if (actions.ContainsKey(action))
        {
            return actions[action](id);
        }
        try
        {
            return GetTextFromJson(action, id);
        }
        catch (KeyNotFoundException)
        {

            return $"Now why would I want to {action} the {interactableName}.";
        }
    }

    //object name + action + ID,  Description
    //object name + action + ID.NONE,  Description

    //appleinspectbrawler, an apple...not as good as booze but its a nice snack
    //applegrab, you grab the apple
    public string GetTextFromJson(String Action, IDCard id)
    {
        if(dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<DialogueManager>();
        }
        
        return dialogueManager.GetDialogue(this.interactableName, Action, id.Name);
    }

    /// <summary>
    /// Gets the description of the interactable based on the IDCard. Needs to be overriden.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual string GetDescription(IDCard id)
    {
        if (GetTextFromJson("inspect", id) != "key not found")
        {
            return GetTextFromJson("inspect", id);
        }
        else if (GetTextFromJson("inspect", IDCard.None) != "key not found")
        {
            return GetTextFromJson("inspect", IDCard.None);
        }
        else
        {
            throw new NotImplementedException();
        }
    }
}