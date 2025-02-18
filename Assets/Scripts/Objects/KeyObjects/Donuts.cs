using UnityEngine;

public class Donuts : Interactable
{

    Donuts() : base()
    {
        // Set the object's name
        SetName("Donuts");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction("eat", Eat);
    }

    private void Eat(IDCard id)
    {
        Destroy(this.gameObject, 100);
        GetComponentInParent<RoomBehavior>().InitIteractables();
        GameManager.Instance().AddTextToJournal(this.GetTextFromJson("eat", id));
    }
}
