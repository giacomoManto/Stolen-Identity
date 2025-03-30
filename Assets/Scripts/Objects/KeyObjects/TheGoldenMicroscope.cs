using System;
using System.Dynamic;
using UnityEngine;

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
        PlayerInfo player = FindFirstObjectByType<PlayerInfo>();
        if (id.Name != IDCard.Thief.Name)
        {
            GameManager.Instance().AddTextToJournal(dialogueManager.GetDialogue(this.interactableName,"take", id.Name));
            return;
        }

        if(!player.getPlayerInventory().ContainsKey("calling card"))
        {
            GameManager.Instance().AddTextToJournal("I reach for the Golden Microscope but hesitate—something this valuable demands a signature touch. If they’re going to lose a treasure, they should at least know who took it. Something flashy, something that would drive the guards mad… my calling card. Until then, I cant steal this microscope.");
            return;
        }
        GameManager.Instance().AddTextToJournal("I take out my calling card, placing it in the glass case with a smug grin. My heists will always rule, I doubt the guards suspect a thing.");
        player.removeItem("calling card");
        base.TakeObject(id);
        GameManager.Instance().SetFlag("Thief Ending", true);
    }
}
