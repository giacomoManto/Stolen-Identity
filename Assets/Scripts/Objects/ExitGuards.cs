using UnityEngine;

public class ExitGuards : Interactable
{
    [SerializeField]
    private GameObject gauze;
    [SerializeField]
    private RoomBehavior brawlerRoom;
    [SerializeField]
    private RoomBehavior patientRoom;
    private void Start()
    {
        this.RegisterAction(Give, "give", "offer", "hand", "present", "deliver", "pass", "provide", "grant", "bestow", "supply");
        this.RegisterAction(TalkTo, "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
        this.RegisterAction(Fight, "fight", "attack", "combat", "battle", "clash", "engage", "assault", "struggle", "spar", "duel","punch","kick");

    }
    public void Fight(IDCard id)
    {
        if (id.Equals(IDCard.Brawler) && !GameManager.Instance().GetFlag("fist wraps") && !GameManager.Instance().GetFlag("booze"))
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("fight", id));
            GameManager.Instance().changeRoom(brawlerRoom);
            GameObject temp = Instantiate(gauze);
            temp.transform.parent = GameManager.Instance().CurrentPlayerRoom.transform;
            GameManager.Instance().CurrentPlayerRoom.InitInteractables();
        }
        else if (!id.Equals(IDCard.Brawler))
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("fight", id));
            
            GameManager.Instance().changeRoom(patientRoom);
        }
        
    }
    public void Give(IDCard id)
    {
        if (FindFirstObjectByType<PlayerInfo>().isItemInInventory("donuts"))
        {
            FindFirstObjectByType<PlayerInfo>().removeItem("donuts");
            GameManager.Instance().AddTextToJournal("I give the donuts to the guards by the door. They jump for joy and dive into the donuts. It's as if the world around them has dissapeared they are totally distracted.");
            GameManager.Instance().SetFlag("guardsDistracted", true);
        }
    }

    public override void InspectObject(IDCard id)
    {
        if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal("The two guards have moved from their post by the door and are now sitting, chatting and enjoying the donuts I gave them.");
        }
        else
        {
            GameManager.Instance().AddTextToJournal("The two guards look pretty stern.");
        }

    }

    public void TalkTo(IDCard id)
    {
        if (GameManager.Instance().GetFlag("guardsDistracted"))
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("talk", id));
        }
        else
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("talkDonuts", id));
        }
        
    }
}
