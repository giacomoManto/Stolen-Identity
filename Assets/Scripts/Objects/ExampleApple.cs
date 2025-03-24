using System;
using UnityEngine;

public class ExampleApple : Interactable
{
    [SerializeField]
    private GameObject crushedApple;

    public ExampleApple() : base()
    {
        this.SetName(interactableName);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.RegisterAction(Lick, "lick", "taste");
        this.RegisterAction(Eat, "eat", "munch", "devour", "consume", "nibble", "chomp", "ingest", "bite", "chew", "snack on");
        this.RegisterAction(Throw, "throw", "toss", "hurl", "fling", "pitch", "lob", "chuck", "cast", "launch", "heave");
    }

    /// <summary>
    /// Lick the apple.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private void Lick(IDCard id)
    {
        GameManager.Instance().AddTextToJournal("I lick the apple. It tastes like an apple.");
    }

    private void Eat(IDCard id)
    {
        GameManager.Instance().AddTextToJournal("I take a large bite out of the crisp apple. Juice drips down my chin as I start to chew. Each bite fills my mouth with an explosion of flavour which reminds me of ... what does it remind me of?");
        FindFirstObjectByType<PlayerInfo>().removeItem(this);
        DestroyImmediate(this.gameObject);
        GameManager.Instance().CurrentPlayerRoom.InitIteractables();
    }

    private void Throw(IDCard id)
    {
        FindFirstObjectByType<PlayerInfo>().removeItem(this);
        GameManager.Instance().AddTextToJournal("I hold the apple firmly in my right hand. I start charging my arm cannon. My arm starts vibrating, I'm squishing the apple. I heave my arm forwards sending the apple flying in a perfect arch. Time seems to slow down as the apple soars through the air, it's juices trailing behind it. I totally miss.");
        GameObject crushedInstance = Instantiate(crushedApple, transform.parent.position, Quaternion.identity);
        crushedInstance.SetActive(true);
        crushedInstance.transform.parent = GameManager.Instance().CurrentPlayerRoom.transform;
        GameManager.Instance().CurrentPlayerRoom.InitIteractables();
        DestroyImmediate(this.gameObject);
    }
}
