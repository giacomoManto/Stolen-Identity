using UnityEngine;

public class FireExit : Interactable
{
    FireExit() : base()
    {
        this.SetName("Fire Exit");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance().SetFlag("fireAlarmDisabled", false);
        this.RegisterAction("go through", GoThrough);
        this.RegisterAction("open", GoThrough);
        this.RegisterAction("use", GoThrough);
        this.RegisterAction("enter", GoThrough);
    }

    private void GoThrough(IDCard id)
    {
        if (GameManager.Instance().GetFlag("fireAlarmDisabled"))
        {
            GameManager.Instance().SetFlag("gameOver", true);
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("go through", id));
            GameManager.Instance().AddTextToJournal("You have succesfully completed the prototype of stolen-identity. Thank you for playing!");
        }
        else
        {
            GameManager.Instance().AddTextToJournal("It's clearly written. Alarm will sound if door opened. No way I can leave through this and not get caught, unless I can figure out how to disable the alarms.");
        }
    }
}
