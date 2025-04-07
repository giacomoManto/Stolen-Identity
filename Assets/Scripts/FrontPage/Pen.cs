using UnityEngine;
using UnityEngine.EventSystems;

public class Pen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform PenImage;
    public RectTransform ShadowImage;
    public RectTransform Book;
    bool transitioned = false;
    bool hover = false;
    Vector3 goalBook;
    Vector3 goalPen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalBook = Book.transform.position;
        goalPen = PenImage.transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transitioned || !hover)
            {
                return;
            }
            SlideBookIn();
        }
        if (transitioned)
        {
            Book.transform.position = Vector3.Lerp(Book.transform.position, goalBook, Time.deltaTime * 5);
            PenImage.transform.position = Vector3.Lerp(PenImage.transform.position, goalPen, Time.deltaTime * 5);
            ShadowImage.transform.position = Vector3.Lerp(ShadowImage.transform.position, goalPen, Time.deltaTime * 5);
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
        PenImage.transform.position = PenImage.transform.position + new Vector3(0, 10, 0);
        ShadowImage.transform.position = ShadowImage.transform.position + new Vector3(0, -10, 0);
        Debug.Log("Pointer Entered Pen");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
        PenImage.transform.position = PenImage.transform.position + new Vector3(0, -10, 0);
        ShadowImage.transform.position = ShadowImage.transform.position + new Vector3(0, 10, 0);
        Debug.Log("Pointer Exited Pen");
    }

    private void SlideBookIn()
    {
        transitioned = true;
        Vector3 diff = PenImage.transform.position - Book.transform.position;
        goalBook = Book.transform.position + diff;
        goalPen = PenImage.transform.position + diff;
        Debug.Log("Clicked");
    }
}
