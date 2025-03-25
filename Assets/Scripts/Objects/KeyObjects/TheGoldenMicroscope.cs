using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.WSA;

public class TheGoldenMicroscope : Interactable
{
    
    TheGoldenMicroscope() : base()
    {
        // Set the object's name
        SetName("The Golden Microscope");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.playerCanStore = true;   
    }
    public override void TakeObject(IDCard id)
    {
        if(id.Name != IDCard.Thief.Name)
        {
            GameManager.Instance().AddTextToJournal(dialogueManager.GetDialogue(this.interactableName,"take", id.Name));
            return;
        }

        if(!FindFirstObjectByType<PlayerInfo>().getPlayerInventory().ContainsKey("Calling Card"))
        {
            GameManager.Instance().AddTextToJournal("I reach for the Golden Microscope but hesitate—something this valuable demands a signature touch. If they’re going to lose a treasure, they should at least know who took it. Something flashy, something that would drive the guards mad… my calling card. Until then, I cant steal this microscope.");
            return;
        }
        base.TakeObject(id);
    }
}
