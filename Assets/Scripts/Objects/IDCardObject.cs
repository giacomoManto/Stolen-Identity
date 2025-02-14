using System;
using UnityEngine;

public class IDCardObject : Interactable
{
    string idName = string.Empty;
    public IDCardObject() : base()
    {
        this.SetName(interactableName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction("take", Take);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public override string GetDescription(IDCard id)
    {
        return "A simple plastic "+ interactableName +" "+ location;
    }

    /// <summary>
    /// Lick the apple.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string Take(IDCard id)
    {
        return "I take the " + interactableName + " and pocket it.";
    }
}
