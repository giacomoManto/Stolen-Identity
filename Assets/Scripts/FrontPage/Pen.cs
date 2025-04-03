using UnityEngine;
using UnityEngine.EventSystems;

public class Pen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public  RectTransform PenImage;
    public RectTransform ShadowImage;
    public RectTransform Book;
    bool transitioned = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (transitioned)
            {
                return;
            }
            SlideBookIn();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        PenImage.transform.position = PenImage.transform.position + new Vector3(0, 10, 0);
        ShadowImage.transform.position = ShadowImage.transform.position + new Vector3(0, -10, 0);
        Debug.Log("Pointer Entered Pen");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PenImage.transform.position = PenImage.transform.position + new Vector3(0, -10, 0);
        ShadowImage.transform.position = ShadowImage.transform.position + new Vector3(0, 10, 0);
        Debug.Log("Pointer Exited Pen");
    }

    private void SlideBookIn()
    {
        transitioned = true;
    }
}
