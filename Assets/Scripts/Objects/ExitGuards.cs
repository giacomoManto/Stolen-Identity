using UnityEngine;

public class ExitGuards : Interactable
{
    private void Start()
    {
        this.RegisterAction(Give, "give", "offer", "hand", "present", "deliver", "pass", "provide", "grant", "bestow", "supply");
        this.RegisterAction(TalkTo, "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
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
