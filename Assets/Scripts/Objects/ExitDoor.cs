using System.Net.Sockets;
using UnityEngine;

public class ExitDoor : Interactable
{
    ExitDoor() : base()
    {
        // Set the object's name
        SetName("Exit Door");
    }

    private void Start()
    {
        this.RegisterAction(GoThrough, "use", "open", "go through", "pass through", "enter", "move through", "walk through", "step through", "proceed through", "traverse", "penetrate", "slip through", "creep through", "push through", "burst through", "barge through", "infiltrate", "tread through");
        this.RegisterAction(InspectObject, "inspect", "look at", "peek at", "stare at");

    }
    private void GoThrough(IDCard id)
    {
        // Do this in order of ending level
        if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal("With the guards distracted I slip through the door and make my way towards freedom.");
            GameManager.Instance().SetFlag("gameOver", true);
            GameManager.Instance().SetFlag("escaped", true);
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("go throughX", id));
        }
        // Add other detections here
        else
        {
            GameManager.Instance().AddTextToJournal("The guards won't let me through. I need to distract them or get the correct permission to get out of here.");
        }
    }


    public override void InspectObject(IDCard id)
    {
        if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal("It's the exit door. I can see the outside world through the glass. I can't wait to get out of here. The guards are still distracted, I should make my move now.");
        }
        else
        {
            GameManager.Instance().AddTextToJournal("It's the exit door. I can see the outside world through the glass. I can't wait to get out of here. Unfortunately two guards look like they are more focused on keeping people in rather than out.");
        }

    }

}
