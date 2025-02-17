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
        GameManager.GetInstance().SetFlag("fireAlarmDisabled", false);
        this.RegisterAction("go through", GoThrough);
        this.RegisterAction("open", GoThrough);
        this.RegisterAction("use", GoThrough);
        this.RegisterAction("enter", GoThrough);
    }

    private string GoThrough(IDCard id)
    {
        if (GameManager.GetInstance().GetFlag("fireAlarmDisabled"))
        {
            GameManager.GetInstance().SetFlag("gameOver", true);
            return this.GetTextFromJson("go through", id);
        }
        else
        {
            return "It's clearly written. Alarm will sound if door opened. No way I can leave through this and not get caught, unless I can figure out how to disable the alarms.";
        }
    }
}
