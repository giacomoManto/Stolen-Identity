using UnityEditor.Tilemaps;
using UnityEngine;

public class Booze : Interactable
{
    [SerializeField]
    SleepingMan man;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //this.RegisterAction();

    }
    public override void TakeObject(IDCard id)
    {
        //IF this already exist in player inventory just ignore everything below
        if (FindAnyObjectByType<PlayerInfo>().isItemInInventory(interactableName))
        {
            base.TakeObject(id);
            return;
        }
        //if the dude is unconcious then take the booze
        if (man.isUnconscious())
        {
            base.TakeObject(id);
            man.boozeExists = false;
            GameManager.Instance().SetFlag("booze", true);
            return;
        }
        //if you are thief take the booze
        else if (id.Equals(IDCard.Thief))
        {
            GameManager.Instance().AddTextToJournal("I stealthly approach the man, knowing he's more alert than he lets on. Still, I swipe the booze from him in record time.");
            base.TakeObject(id);
            man.boozeExists = false;
            GameManager.Instance().SetFlag("booze", true);
            return;
        }
        man.Attack(id);



    }

    


}
