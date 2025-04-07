using UnityEngine;
using UnityEngine.EventSystems;

public class Pen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform PenImage;
    public RectTransform ShadowImage;
    public RectTransform Book;
    public bool transitioned = false;
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
            FindAnyObjectByType<StartSceneController>().OnClickPen();
            transitioned = true;
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
}
