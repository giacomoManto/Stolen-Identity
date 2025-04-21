using UnityEngine;

public class GameQuitter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void CloseGame()
    {
        Debug.Log("Game is closing...");
        Application.Quit();
    }
}
