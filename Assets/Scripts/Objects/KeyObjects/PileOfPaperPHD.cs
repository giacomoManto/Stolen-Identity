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

    string Search(IDCard idCard)
    {
        if (searched)
        {
            return "You find nothing of interest in the pile of paper.";
        }
        GameObject PHDCopy = Instantiate(PHD, transform.parent.position, Quaternion.identity);
        PHDCopy.SetActive(true);
        PHDCopy.transform.parent = transform.parent;
        GetComponentInParent<RoomBehavior>().InitIteractables();
        return "You find a PHD in the pile of paper.";
    }
}
