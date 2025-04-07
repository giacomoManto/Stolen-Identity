using TMPro;
using UnityEngine;

public class BookCover : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;

    bool nameSet = false;

    public void setName(string name)
    {
        nameText.text = name;
        nameSet = true;
    }


    void OnGUI()
    {
        if (nameSet)
        {
            return;
        }
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
        {
            return;
        }
        if (e.keyCode == KeyCode.Return)
        {
            if (nameText.text.Length <= 0)
            {
                return;
            }
            FindAnyObjectByType<StartSceneController>().setName(nameText.text);
            nameSet = true;
        }
        if (e.keyCode == KeyCode.Backspace)
        {
            if (nameText.text.Length <= 0)
            {
                return;
            }
            nameText.text += nameText.text.Substring(0, nameText.text.Length - 1);
        }
        else
        {
            char c = e.character;
            if (!char.IsControl(c))
            {
                nameText.text += c;
                nameText.text = nameText.text.TrimStart();
            }
        }
    }
}
