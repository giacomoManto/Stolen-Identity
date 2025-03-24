using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DoorBehavior : Interactable
{
    [SerializeField]
    private RoomBehavior room;

    [SerializeField]
    private int lockLevel = 0;

    private bool locked = true;

    private void Start()
    {
        if (lockLevel == 0)
        {
            this.locked = false;
        }

        // Action Registration
        this.RegisterAction(GoThrough, "use", "open", "go through", "pass through", "enter", "move through", "walk through", "step through", "proceed through", "traverse", "penetrate", "slip through", "creep through", "push through", "burst through", "barge through", "infiltrate", "tread through");
        this.RegisterAction("lockpick", Lockpick);
        this.RegisterAction("breakdown", Breakdown);
        this.RegisterAction("break", Breakdown);
    }

    public DoorBehavior() : base()
    {
        this.SetName(interactableName);
    }



    private void GoThrough(IDCard id)
    {
        if (!locked) {
            GameManager.Instance().AddTextToJournal("You go through the door.");
            GameManager.Instance().changeRoom(room);
        }
        else
        {
            GameManager.Instance().AddTextToJournal("PLACEHOLDER: You try the handle but the door is locked.");
        }
    }

    private void Lockpick(IDCard id) {
        if (id.Equals(IDCard.Thief)) {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("PLACEHOLDER: You skillfully lockpick the door. Unlocking it");
            }
            else
            {
                GameManager.Instance().AddTextToJournal("PLACEHOLDER: You spend a couple minutes inserting, twisting and applying pressure to the door lock. This must be the hardest lock you have ever picked. In a bout of frustration you throw your tools on the ground, your alan key bounces up and hits the door knob revealing that it was unlocked the whole time.");
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal("PLACEHOLDER: I don't know how to do that.");
        }
    }

    private void Breakdown(IDCard id) {
        if (id.Equals(IDCard.Brawler))
        {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("PLACEHOLDER: You smash a big hole where the door knob used to be.");
            }
            else
            {
                GameManager.Instance().AddTextToJournal("PLACEHOLDER: You break a hole in the door. It was unlocked before you did this.");
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal("PLACEHOLDER: I'm not strong enough for that.");
        }
    }

    }
