using UnityEngine;

public class FireAlarm : Interactable
{

    private static int totalFireAlarms = 0;
    private static int disabledFireAlarms = 0;
    private bool isDisabled = false;

    public FireAlarm() : base()
    {
        this.SetName("Fire Alarm");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCanStore = false;
        totalFireAlarms += 1;
        this.RegisterAction("disable", Disable);
        this.RegisterAction("break", Disable);
        this.RegisterAction("destroy", Disable);
    }

    private void Disable(IDCard id)
    {
        if (isDisabled)
        {
            GameManager.Instance().AddTextToJournal("The fire alarm is already disabled.");
        } else
        {
            isDisabled = true;
            disabledFireAlarms += 1;
            if (disabledFireAlarms == totalFireAlarms)
            {
                GameManager.Instance().SetFlag("fireAlarmDisabled", true);
                GameManager.Instance().AddTextToJournal("I disable the fire alarm. All fire alarms are now disabled. This might let me get out of here.");
            }
            else
            {
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("disable", id));
            }
        }
        
    }
}
