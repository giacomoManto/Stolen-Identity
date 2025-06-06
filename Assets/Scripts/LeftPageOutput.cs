using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LeftPageOutput : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Room Names that corrispond to the room sprites of the same index.")]
    private List<string> correspondingRoomNames = new List<string>();
    [SerializeField]
    private List<Sprite> allRoomSprites = new List<Sprite>(1);
    

    private static LeftPageOutput instance;

    private TMP_Text viewableText;
    private UnityEngine.UI.Image roomSketch;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        viewableText = GetComponentInChildren<TMP_Text>();
        roomSketch = GetComponentInChildren<UnityEngine.UI.Image>();
    }
    public static LeftPageOutput Instance()
    {
        return instance;
    }

    public void updatePage(string roomName)
    {
        viewableText.text = roomName;
        if (correspondingRoomNames.Contains(roomName))
        {
            roomSketch.sprite = allRoomSprites.ToArray()[correspondingRoomNames.IndexOf(roomName)];
        }
        else
        {
            roomSketch.sprite = allRoomSprites[0];
        }

    }
}
