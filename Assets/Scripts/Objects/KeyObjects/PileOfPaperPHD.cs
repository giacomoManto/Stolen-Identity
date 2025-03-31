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
        this.RegisterAction(Search,"search", "inspect");
    }

    private void Search(IDCard idCard)
    {
        if (searched)
        {
            GameManager.Instance().AddTextToJournal("I find nothing of interest in the pile of paper.");
        }
        else
        {
            GameObject PHDCopy = Instantiate(PHD, transform.parent.position, Quaternion.identity);
            PHDCopy.SetActive(true);
            PHDCopy.transform.parent = transform.parent;
            GetComponentInParent<RoomBehavior>().InitInteractables();
            GameManager.Instance().AddTextToJournal("Bafflingly, I find a PHD in the pile of paper after going through it. Who would ever put this in here?");
        }
    }
}
