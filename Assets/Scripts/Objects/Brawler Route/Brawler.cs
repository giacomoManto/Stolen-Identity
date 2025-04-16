using UnityEngine;

public class Brawler : Interactable
{
    [SerializeField]
    GameObject punchCardPunched;


    [SerializeField]
    RoomBehavior startingRoom;

    int talkTuahCount = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(TalkTo, "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
        this.RegisterAction(Annoy, "wake", "annoy", "tickle", "irritate", "bother", "pester", "provoke", "agitate", "disturb", "bug", "harass", "vex", "exasperate", "irk", "madden", "ruffle", "needle", "hassle", "plague", "torment", "badger", "nag", "molest");
    }

    private void TalkTo(IDCard id)
    {
        if (id.Name == IDCard.Brawler.Name)
        {
            GameManager.Instance().AddTextToJournal("I don't walk to talk to myself. What would I even say? Although I admit I would be a good drinking partner for myself.");
            return;
        }
        switch (talkTuahCount)
        {
            case(0):
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("talk1", id));
                break;
            case(1):
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("talk2", id));
                break;
            case(3):
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("talk3", id));
                break;
            default:
                GameManager.Instance().AddTextToJournal(this.GetTextFromJson("talkToAnnoy", id));
                Annoy(id);
                break;
        }
        talkTuahCount++;
    }

    private void Annoy(IDCard id)
    {
        if (GameManager.Instance().GetFlag("hasPunchcard"))
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyPunchCard", id));
            GameObject punchCardPunchedInstance = Instantiate(punchCardPunched);
            GameManager.Instance().addObjectToPlayerInventory(punchCardPunchedInstance.GetComponent<Interactable>());
            FindFirstObjectByType<PlayerInfo>().removeItem("punchcard");
            GameManager.Instance().SetFlag("hasPunchedPunchcard", true);
            GameManager.Instance().SetFlag("hasPunchcard", false);
        }
        else
        {
            GameManager.Instance().AddTextToJournal(this.GetTextFromJson("annoyNoPunchCard", id));
            GameManager.Instance().changeRoom(startingRoom);
        }
    }
}
