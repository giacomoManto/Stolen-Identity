using UnityEngine;

public class LabCoat : Interactable
{

    bool attemptedToPutOnAsBrawler = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance().SetFlag("LabCoat", false);
        this.RegisterAction(PutOn, "use", "put on", "wear", "don", "dress in", "slip on", "pull on", "throw on", "clothe in", "robe in", "get into", "try on", "enrobe in");
    }


    private void PutOn(IDCard id)
    {
        if (id.Name == IDCard.Brawler.Name)
        {
            if (attemptedToPutOnAsBrawler)
            {
                GameManager.Instance().AddTextToJournal("I pull the tiny lab coat over my arms. As I bring my arms together I hear a ripping sound. I turn around and find the labcoat ripped to pieces on the floor. I guess my muscles are just too big.");
                RoomBehavior parent = this.transform.parent.GetComponent<RoomBehavior>();
                transform.parent = null;
                parent.InitInteractables();
                Destroy(this);
                return;
            }
            attemptedToPutOnAsBrawler = true;
            GameManager.Instance().AddTextToJournal("I could try to put this on but it might rip it. I should be careful.");
            return;
        }
        else
        {
            GameManager.Instance().AddTextToJournal(GetTextFromJson("put on", id));
            if (id.Name == IDCard.Doctor.Name)
            {
                GameManager.Instance().SetFlag("LabCoat", true);
                RoomBehavior parent = this.transform.parent.GetComponent<RoomBehavior>();
                transform.parent = null;
                parent.InitInteractables();
                Destroy(this);
            }
        }
    }
}
