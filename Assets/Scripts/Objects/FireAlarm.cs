using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : Interactable
{

    private static int totalFireAlarms = 0;
    private static int disabledFireAlarms = 0;
    private bool isDisabled = false;
    private bool attemptedPull = false;

    public FireAlarm() : base()
    {
        this.SetName("Fire Alarm");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCanStore = false;
        totalFireAlarms += 1;
        this.RegisterAction(Disable, "disable", "break", "destroy", "smash", "hit", "kick", "punch");
        this.RegisterAction(Pull, "pull", "activate", "trigger", "engage", "set off", "trip", "deploy", "yank");
    }

    private void Disable(IDCard id)
    {
        if (isDisabled)
        {
            GameManager.Instance().AddTextToJournal("The fire alarm is already disabled.");
        }
        else
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

    private void Pull(IDCard id)
    {
        if (isDisabled)
        {
            GameManager.Instance().AddTextToJournal("I pull the alarm. No alarms go off. Oh right ... I disabled it already, that seems kind of dangerous.");
            return;
        }
        if (!attemptedPull)
        {
            GameManager.Instance().AddTextToJournal("I really don't think I should do that. The alarm will go off and draw attention to me.");
            attemptedPull = true;
        }
        else
        {
            GameManager.Instance().AddTextToJournal("I pull the alarm. The alarm goes off. Suddenly I am surrounded by security. I feel a sharp pain across the back of my head and crumple to the floor.");
            GameManager.Instance().SetFlag("gameOver", true);
            GameManager.Instance().SetFlag("fail", true);
        }
    }
}
