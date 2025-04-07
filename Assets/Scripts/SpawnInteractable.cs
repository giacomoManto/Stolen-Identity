using System.Collections.Generic;
using UnityEngine;

public class SpawnInteractable : Interactable
{
    [SerializeField]
    bool destroyOnSpawn = false;

    [SerializeField]
    private GameObject[] spawnedObjects;

    [SerializeField]
    private string spawnedText;

    [SerializeField]
    private List<string> actionsWhichTriggerSpawn = new List<string>();

    public enum IDCardC
    {
        None,
        Thief,
        Patient,
        Brawler,
        Guard,
        Doctor
    }
    public IDCardC requiredId = IDCardC.None;

    private bool objectSpawned = false;


    /*
     * For this class to work you need
     * 1. 'object spawned' text. If the object has already spawned what will this object say. ie if the pile of paper is searched you could say
     * theres nothing left in this pile of paper
     * 
     * 2. 'on spawn' 
     */
    SpawnInteractable() : base()
    {

    }

    private IDCard getIDFromIDCardC(IDCardC idCardC)
    {
        switch (idCardC)
        {
            case IDCardC.None:
                return IDCard.None;
            case IDCardC.Thief:
                return IDCard.Thief;
            case IDCardC.Patient:
                return IDCard.Patient;
            case IDCardC.Brawler:
                return IDCard.Brawler;
            case IDCardC.Guard:
                return IDCard.Guard;
            case IDCardC.Doctor:
                return IDCard.Doctor;
            default:
                return IDCard.None;
        }
    }

    private void Start()
    {
        foreach (string action in actionsWhichTriggerSpawn)
        {
            RegisterAction(action, SpawnObject);
        }
    }



    private void SpawnObject(IDCard idCard)
    {
        if (requiredId != IDCardC.None)
        {
            IDCard required = getIDFromIDCardC(requiredId);
            if (idCard.Name != required.Name)
            {
                GameManager.Instance().AddTextToJournal("Someone else may be able to do that but definitely not me.");
                return;
            }
        }
        string gameMangagerText = "";
        if (objectSpawned)
        {
            gameMangagerText = dialogueManager.GetDialogue(interactableName, "object spawned", IDCard.None.Name);
            if (gameMangagerText == "")
            {
                gameMangagerText = "I think before realizing nothing would come of doing that again.";
            }
            GameManager.Instance().AddTextToJournal(gameMangagerText);
        }
        else
        {
            foreach(GameObject spawnedObject in spawnedObjects)
            {
                GameObject spawnedObjectCopy = Instantiate(spawnedObject, transform.parent.position, Quaternion.identity);
                spawnedObjectCopy.SetActive(true);
                spawnedObjectCopy.transform.parent = transform.parent;
                GetComponentInParent<RoomBehavior>().InitInteractables();
                gameMangagerText = dialogueManager.GetDialogue(interactableName, "on spawn", IDCard.None.Name);
                GameManager.Instance().AddTextToJournal(gameMangagerText);
            }
           
            objectSpawned = true;
        }
        if (destroyOnSpawn)
        {
            GameManager.Instance().DestroyInteractble(this.gameObject);
        }
    }
}
