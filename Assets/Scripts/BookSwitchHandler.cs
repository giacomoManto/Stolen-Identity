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
        Color desiredColor;
        // FIWB Run it every fixed update idc
        switch (player.getPlayerID().ToString().ToLower())
        {
            case "doctor":
                desiredColor = doctorColor;
                break;
            case "brawler":
                desiredColor = brawlerColor;
                break;
            case "patient":
                desiredColor = patientColor;
                break;
            case "thief":
                desiredColor = thiefColor;
                break;
            case "guard":
                desiredColor = guardColor;
                break;
            default:
                desiredColor = defaultColor;
                break;
        }
        GetComponent<Image>().color = Vector4.Lerp(GetComponent<Image>().color, desiredColor, 0.01f); // Lerp the color of the image to the desired color
    }
}
