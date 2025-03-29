using System;
using UnityEngine;

public class IDCardObject : Interactable
{
    public IDCardObject() : base()
    {
        this.SetName(interactableName);
        this.RegisterAction(SwitchId, "use");

    }
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SwitchId(IDCard id)
    {
        FindAnyObjectByType<PlayerInfo>().switchPlayerID(interactableName);
    }



}
