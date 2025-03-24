using UnityEngine;
using UnityEngine.UI;

public class BookSwitchHandler : MonoBehaviour
{
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Color doctorColor;
    [SerializeField]
    private Color brawlerColor;
    [SerializeField]
    private Color patientColor;
    [SerializeField]
    private Color thiefColor;
    [SerializeField]
    private Color guardColor;

    PlayerInfo player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerInfo>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // FIWB Run it every fixed update idc
        switch (player.getPlayerID().ToString().ToLower())
        {
            case "doctor":
                GetComponent<Image>().color = doctorColor;
                break;
            case "brawler":
                GetComponent<Image>().color = brawlerColor;
                break;
            case "patient":
                GetComponent<Image>().color = patientColor;
                break;
            case "thief":
                GetComponent<Image>().color = thiefColor;
                break;
            case "guard":
                GetComponent<Image>().color = guardColor;
                break;
            default:
                GetComponent<Image>().color = defaultColor;
                break;
        }
    }
}
