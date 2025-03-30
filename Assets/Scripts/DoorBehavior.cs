using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class DoorBehavior : Interactable
{
    [SerializeField]
    private RoomBehavior room;

    [SerializeField]
    private int lockLevel = 0;

    [SerializeField]
    private string roomName = "";

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
        assignRoomIfNone();
        if (!locked) {
            GameManager.Instance().AddTextToJournal("I go through the " + interactableName + ".");
            GameManager.Instance().changeRoom(room);
        }
        else
        {
            if (id.GetSecurityLevel() >= lockLevel)
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson("door", "unlock", id));
                GameManager.Instance().changeRoom(room);
            }
            else
            {
                GameManager.Instance().AddTextToJournal(GetTextFromJson(interactableName, "unlockFail", id));
            }
        }
    }

    private void Lockpick(IDCard id) {
        assignRoomIfNone();
        if (id.Equals(IDCard.Thief)) {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("I skillfully lockpick the door. With a satisfying click the door unlocks completely.");
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
        assignRoomIfNone();
        if (id.Equals(IDCard.Brawler))
        {
            if (this.locked)
            {
                this.locked = false;
                GameManager.Instance().AddTextToJournal("I smash a big hole where the door knob used to be. A door cant be locked if the locking stuff is not in the door.");
            }
            else
            {
                GameManager.Instance().AddTextToJournal("I break a hole in the door. It was probably unlocked before I did that but who cares? Its fun to break stuff.");
            }
        }
        else
        {
            GameManager.Instance().AddTextToJournal("As much as I hate to admit it, I'm not strong enough for that.");
        }
    }
    private void assignRoomIfNone()
    {
        if (roomName != "" && room == null)
        {
            room = GameManager.Instance().GetRoomByName(roomName);
        }
    }
    }
