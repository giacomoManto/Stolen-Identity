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
            GameManager.Instance().AddTextToJournal("I go through the door.");
            GameManager.Instance().changeRoom(room);
        }
        else
        {
            GameManager.Instance().AddTextToJournal("I try the handle but the door is locked.");
        }
    }

    private void Lockpick(IDCard id) {
        if (id.Equals(IDCard.Thief)) {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("I skillfully lockpick the door. Unlocking it");
            }
            else
            {
                GameManager.Instance().AddTextToJournal("I spend a couple minutes inserting, twisting and applying pressure to the door lock. This must be the hardest lock I have ever picked. In a bout of frustration I throw my tools on the ground, my alan key bounces up and hits the door knob revealing that it was unlocked the whole time.");
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal("I don't know how to do that.");
        }
    }

    private void Breakdown(IDCard id) {
        if (id.Equals(IDCard.Brawler))
        {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("I smash a big hole where the door knob used to be.");
            }
            else
            {
                GameManager.Instance().AddTextToJournal("I break a hole in the door before realizing it was unlocked before I did this. Oh well, the door deserved it anyway.");
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal("PLACEHOLDER: I'm not strong enough for that.");
        }
    }

    }
