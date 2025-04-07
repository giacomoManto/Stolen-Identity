using UnityEngine;

public class StartSceneController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 50.0f;

    public RectTransform Pen;
    public RectTransform BookCover;
    public RectTransform Book;

    private Vector3 penGoal;
    private Vector3 bookCoverGoal;
    private Vector3 bookGoal;

    SaveData gameData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        penGoal = Pen.transform.position;
        bookCoverGoal = BookCover.transform.position;
        bookGoal = Book.transform.position;
        gameData = SaveDataManager.Instance().LoadGame();
        if (gameData.playerName != "John Random")
        {
            FindAnyObjectByType<BookCover>().setName(gameData.playerName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Pen.transform.position = Vector3.MoveTowards(Pen.transform.position, penGoal, Time.deltaTime * moveSpeed);
        BookCover.transform.position = Vector3.MoveTowards(BookCover.transform.position, bookCoverGoal, Time.deltaTime * moveSpeed);
        Book.transform.position = Vector3.MoveTowards(Book.transform.position, bookGoal, Time.deltaTime * moveSpeed);
    }

    public void setName(string name)
    {
        gameData.playerName = name;
        SaveDataManager.Instance().SaveGame(gameData);
    }

    public void OnClickPen()
    {
        Vector3 diff = Pen.transform.position - BookCover.transform.position;
        bookCoverGoal = BookCover.transform.position + diff;
        penGoal = Pen.transform.position + diff;
        Debug.Log("Clicked");
    }
}
