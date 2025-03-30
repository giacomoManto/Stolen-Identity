using System;
using UnityEngine;

public class Donuts : Interactable
{
    public RoomBehavior lobbyRoom;

    public GameObject guardID;

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
        try
        {
            GetComponentInParent<RoomBehavior>().InitInteractables();
        }
        catch (NullReferenceException)
        {
            // Do nothing
        }
        GameObject guardIDInstance = Instantiate(this.guardID, transform.parent.position, Quaternion.identity);
        guardIDInstance.SetActive(true);
        guardIDInstance.transform.parent = FindFirstObjectByType<PlayerInfo>().transform;
        FindFirstObjectByType<PlayerInfo>().addItem(guardIDInstance.GetComponent<Interactable>());
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("eat", id));
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("discoverID", id));

        FindFirstObjectByType<PlayerInfo>().removeItem(this);
        // Destroy the eaten donouts
        GameManager.Instance().DestroyInteractble(gameObject);

    }

    private void Give(IDCard id)
    {
        if (GameManager.Instance().CurrentPlayerRoom == lobbyRoom)
        {
            GameManager.Instance().AddTextToJournal("I give the donuts to the guards by the door. They jump for joy and dive into the donuts. It's as if the world around them has dissapeared they are totally distracted.");
            GameManager.Instance().SetFlag("guardsDistracted", true);
            GameManager.Instance().DestroyInteractble(gameObject);
        }
        else
        {
            GameManager.Instance().AddTextToJournal("There is no-one who would want the donuts here. Maybe I could give them to someone as a distraction.");
        }
    }
}
