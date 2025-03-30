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
            GameManager.Instance().SetFlag("escaped", true);
            if (ThiefEnding(id)) { return; } //If they have the thief id equipped and the ending is possible, leave.
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("go through", id));
        }
        else
        {
            GameManager.Instance().AddTextToJournal("It's clearly written. Alarm will sound if door opened. No way I can leave through this and not get caught, unless I can figure out how to disable the alarms.");
        }
    }
    private bool ThiefEnding(IDCard id)
    {
        if(!GameManager.Instance().GetFlag("Thief Ending") || id.Name != IDCard.Thief.Name)
        {
            return false;
        }
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("thief ending", id));
        return true;
    }
}
