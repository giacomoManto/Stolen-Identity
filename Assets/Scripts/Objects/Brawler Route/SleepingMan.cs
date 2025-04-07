using UnityEngine;

public class SleepingMan : Interactable
{
    [SerializeField]
    RoomBehavior startingRoom;

    protected bool unconscious = false;
    public bool boozeExists = true;
    int annoyCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(Annoy, "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
        this.RegisterAction(Annoy, "wake", "awaken", "waken", "rouse", "stir", "get up");
        this.RegisterAction(Annoy, "nudge", "poke", "prod", "slap");
        this.RegisterAction(Attack, "fight", "brawl", "attack", "punch", "break", "destroy", "kick", "murder", "kill");
    }

    private void Annoy(IDCard id)
    {
        if (unconscious)
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("unconscious", id));
            return;
        }
        if (annoyCounter < 2)
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("talk", id));
        }
        else
        {
            if (!id.Equals(IDCard.Brawler))
            {
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyTalk", id));
                GameManager.Instance().changeRoom(startingRoom);
            }
            else
            {
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyTalkBrawler", id));
                unconscious = true;
            }
           
        }
        annoyCounter++;
    }
    public void Attack(IDCard id)
    {
        if (unconscious)
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("unconscious", id));
            return;
        }
        if (!id.Equals(IDCard.Brawler))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyTalk", id));
            GameManager.Instance().changeRoom(startingRoom);
        }
        else
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyTalkBrawler", id));
            unconscious = true;
        }
    }
    public bool isUnconscious()
    {
        return unconscious;
    }
    public override string GetDescription(IDCard id)
    {
        if (!unconscious)
        {
            return base.GetDescription(id);
        }
        return "The sleeping man remains sleeping across the 3 seats.";


    }
    public override void InspectObject(IDCard id)
    {
        if (!boozeExists && !unconscious)
        {
            GameManager.Instance().AddTextToJournal("The sleeping man remains asleep, snoozing away across 3 of the seats with his now empty brown paper bag.");
            return;
        }
        if (!unconscious)
        {
            base.InspectObject(id);
        }
        GameManager.Instance().AddTextToJournal("This guy is still unconscious, nothing else stands out that much.");
    }

}
