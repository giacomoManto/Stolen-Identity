using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookCover : MonoBehaviour
{
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    Button continueButton;

    bool textEnabled = false;

    private string enteredName = "";

    public void setName(string name)
    {
        enteredName = name.Trim();
        nameText.text = "By:\n" + enteredName;
    }

    public void enableText(bool val)
    {
        textEnabled = val;
    }

    public void saveName()
    {
        FindAnyObjectByType<StartSceneController>().setName(enteredName);
    }

    public void FixedUpdate()
    {
        if (enteredName.Length <= 0)
        {
            continueButton.enabled = false;
            continueButton.GetComponent<Image>().enabled = false;
        }
        else
        {
            continueButton.enabled = true;
            continueButton.GetComponent<Image>().enabled = true;
        }
        nameText.text = "By:\n" + enteredName;
    }



    void OnGUI()
    {
        if (!textEnabled)
        {
            return;
        }
        Event e = Event.current;
        if (e.type != EventType.KeyDown)
        {
            return;
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
