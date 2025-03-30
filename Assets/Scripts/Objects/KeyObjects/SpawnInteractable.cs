using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class SpawnInteractable : Interactable
{
    [SerializeField]
    private GameObject spawnedObject;

    [SerializeField]
    private string spawnedText;

    [SerializeField]
    private List<string> actionsWhichTriggerSpawn = new List<string>();

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

    private void Start()
    {
        foreach (string action in actionsWhichTriggerSpawn)
        {
            RegisterAction(action, SpawnObject);
        }
    }



    private void SpawnObject(IDCard idCard)
    {
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
            GameObject spawnedObjectCopy = Instantiate(spawnedObject, transform.parent.position, Quaternion.identity);
            spawnedObjectCopy.SetActive(true);
            spawnedObjectCopy.transform.parent = transform.parent;
            GetComponentInParent<RoomBehavior>().InitInteractables();
            gameMangagerText = dialogueManager.GetDialogue(interactableName, "on spawn", IDCard.None.Name);
            GameManager.Instance().AddTextToJournal(gameMangagerText);
        }
    }
}
