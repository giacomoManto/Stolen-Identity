using UnityEngine;

public class Donuts : Interactable
{
    public RoomBehavior lobbyRoom;

    Donuts() : base()
    {
        // Set the object's name
        SetName("Donuts");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(Eat, "eat", "munch", "devour", "consume", "ingest", "nibble", "feast", "chomp", "chew", "dine", "snack", "gobble", "partake", "swallow", "taste", "savor", "bite", "sup", "graze", "gulp");
        this.RegisterAction(Give, "give", "donate", "present", "offer", "grant", "bestow", "confer", "provide", "supply", "deliver", "hand", "contribute", "impart", "yield", "allocate", "dispense", "assign", "entrust", "furnish", "award");
    }

    private void Eat(IDCard id)
    {
        Destroy(this.gameObject, 100);
        GetComponentInParent<RoomBehavior>().InitIteractables();
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("eat", id));
    }

    private void Give(IDCard id)
    {
        if (GameManager.Instance().CurrentPlayerRoom == lobbyRoom)
        {
            GameManager.Instance().AddTextToJournal("I give the donuts to the guards by the door. They jump for joy and dive into the donuts. It's as if the world around them has dissapeared they are totally distracted.");
            GameManager.Instance().SetFlag("guardsDistracted", true);
            DestroyImmediate(this.gameObject);
        }
        else
        {
            GameManager.Instance().AddTextToJournal("There is no-one who would want the donuts here. Maybe I could give them to someone as a distraction.");
        }
    }
}
