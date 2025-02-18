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
    private string Lick(IDCard id)
    {
        return "You lick the apple. It tastes like an apple.";
    }
}
