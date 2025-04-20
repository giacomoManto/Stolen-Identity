using UnityEngine;

public class Clerk : Interactable
{
    [SerializeField]
    GameObject punchcard;

    bool given = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(TalkTo, "talk", "talk to", "speak to", "converse with", "chat with", "communicate with", "address", "engage with", "discuss with", "confer with", "have a word with");
    }


    private void TalkTo(IDCard id)
    {

        if (id.Name == IDCard.Doctor.Name)
        {
            if (!given)
            {
                given = true;
                GameObject punchcardInstance = Instantiate(punchcard);
                GameManager.Instance().addObjectToPlayerInventory(punchcardInstance.GetComponent<Interactable>());
                GameManager.Instance().AddTextToJournal("The clerk looks up from thier work and says 'Oh, hello doctor. I've been expecting you. Here's your <b>punchcard</b>.' and turns back to their work.");
                GameManager.Instance().AddTextToJournal("As I turn away the clerk says without looking up, 'Don't forget to get it punched. Those guards by the door are strict with those sort of things.'.");
                GameManager.Instance().SetFlag("hasPunchcard", true);
            }
            else
            {
                GameManager.Instance().AddTextToJournal("The clerk is busy at work and, without looking up, puts a finger to my lips before I can distract them with a conversation.");
            }
        }
        else if (id.Name == IDCard.Guard.Name)
        {
            GameManager.Instance().AddTextToJournal("The clerk looks up at me and says 'What are you doing here? I already gave you your punchcard today. Anyways, if you see the Doctor let them know they forgot to collect their punchcard.");
        }
        else
        {
            GameManager.Instance().AddTextToJournal("The clerk looks super busy at work but looks up at me for a second. I see their eyes glaze over and shift back to their work as they don't recognize me.");
            GameManager.Instance().AddTextToJournal("As I turn away I hear them mutter under their breath 'I swear, if one more person that doesn't need a punchcard comes up to me today...'");
        }
    }
}
