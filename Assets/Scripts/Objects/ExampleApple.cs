using UnityEngine;

public class ExampleApple: Interactable
{
    public ExampleApple(): base()
    {
        this.SetName("Apple");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction("lick", Lick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override string GetDescription(IDCard id)
    {
        return "A juicy delicious red apple " + location;
    }

    /// <summary>
    /// Lick the apple.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private string Lick(IDCard id)
    {
        return "You lick the apple. It tastes like an apple.";
    }
}
