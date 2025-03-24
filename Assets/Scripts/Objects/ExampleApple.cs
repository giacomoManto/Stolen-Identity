using System;
using UnityEngine;

public class ExampleApple: Interactable
{
    public ExampleApple(): base()
    {
        this.SetName(interactableName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction("lick", Lick);
    }


    /// <summary>
    /// Lick the apple.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private void Lick(IDCard id)
    {
        GameManager.Instance().AddTextToJournal("I lick the apple. It tastes like an apple.");
    }
}
