using TMPro;
using UnityEngine;

public class BookCover : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    private string enteredName = "";

    bool nameSet = false;

    public void setName(string name)
    {
        enteredName = name.Trim();
        nameText.text = "By:\n" + enteredName;
        nameSet = true;
    }

    public void FixedUpdate()
    {
        if (nameSet)
        {
            return;
        }
        nameText.text = "By:\n" + enteredName;
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
            if (enteredName.Length <= 0)
            {
                return;
            }
            FindAnyObjectByType<StartSceneController>().setName(enteredName);
            nameSet = true;
        }
        if (e.keyCode == KeyCode.Backspace)
        {
            if (enteredName.Length <= 0)
            {
                return;
            }
            enteredName = enteredName.Substring(0, enteredName.Length - 1);
        }
        else
        {
            char c = e.character;
            if (!char.IsControl(c))
            {
                enteredName += c;
                enteredName = enteredName.TrimStart();
            }
        }
    }
}
