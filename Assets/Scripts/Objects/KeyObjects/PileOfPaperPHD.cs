using UnityEngine;

public class PileOfPaperPHD : Interactable
{

    [SerializeField]
    private GameObject PHD;

    private bool searched = false;

    PileOfPaperPHD() : base()
    {
        SetName("Pile of Paper");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction("search", Search);
    }

    private void Search(IDCard idCard)
    {
        if (searched)
        {
            GameManager.Instance().AddTextToJournal("You find nothing of interest in the pile of paper.");
        }
        else
        {
            GameObject PHDCopy = Instantiate(PHD, transform.parent.position, Quaternion.identity);
            PHDCopy.SetActive(true);
            PHDCopy.transform.parent = transform.parent;
            GetComponentInParent<RoomBehavior>().InitIteractables();
            GameManager.Instance().AddTextToJournal("You find a PHD in the pile of paper.");
        }
    }
}
