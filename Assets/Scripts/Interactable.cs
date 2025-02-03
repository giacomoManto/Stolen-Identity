using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactableName;

    private Dictionary<string, Func<IDCard, string>> actions = new Dictionary<string, Func<IDCard, string>>();

    [SerializeField]
    protected string location;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public Interactable(string name)
    {
        interactableName = name ?? throw new ArgumentNullException(nameof(name));
        this.RegisterAction("inspect", GetDescription);
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
        if (actions.ContainsKey(action))
        {
            return actions[action](id);
        }
        else
        {
            // Maybe add different responses based on the IDCard.
            return $"Now why would I want to {action} the {interactableName}.";
        }
    }

    /// <summary>
    /// Gets the description of the interactable based on the IDCard. Needs to be overriden.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public abstract string GetDescription(IDCard id);
}