using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    // Flag to indicate when dialogue has finished loading.
    public bool IsDialogueLoaded { get; private set; } = false;

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> dialogueData;
    private string dataDirPath = Application.dataPath + "/Dialogue";
    private string dataFileName = "DialogueData";

    private void Awake()
    {
        // Setup singleton instance.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            return;
        }

        dialogueData = ReadDialogueFromFile();
        DebugDialogueData();

        // Mark dialogue as loaded.
        IsDialogueLoaded = true;
    }

    public string GetDialogue(string itemName, string action, string id)
    {
        try
        {
            if (dialogueData.ContainsKey(itemName) &&
                dialogueData[itemName].ContainsKey(action) &&
                dialogueData[itemName][action].ContainsKey(id))
            {
                return dialogueData[itemName][action][id];
            }
            else if (dialogueData.ContainsKey(itemName) &&
                     dialogueData[itemName].ContainsKey(action) &&
                     dialogueData[itemName][action].ContainsKey("Any"))
            {
                return dialogueData[itemName][action]["Any"];
            }
            else
            {
                throw new KeyNotFoundException($"No entry in dialogue for [{itemName}][{action}][{id}]");
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Error occurred when parsing dialogue for item: {itemName}, action: {action}, id: {id}. Message: {e.Message}");
        }
        return "";
    }

    private Dictionary<string, Dictionary<string, Dictionary<string, string>>> ReadDialogueFromFile()
    {
        string filePath = Path.Combine(dataDirPath, dataFileName);
        var allDialogue = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

        try
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError("Dialogue file not found: " + filePath);
                return allDialogue;
            }
            int lineNum = 0;
            foreach (string line in File.ReadLines(filePath))
            {
                lineNum++;
                if (line.StartsWith("//") || line.StartsWith("#"))
                    continue;
                if (line.Length == 0)
                    continue;

                string[] parts = line.Split(':');
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = parts[i].Trim();
                }

                if (parts.Length != 4)
                {
                    Debug.LogError($"Skipping malformed line ({lineNum}): {line}");
                    continue;
                }

                string itemName = parts[0];
                string action = parts[1];
                string id = parts[2];
                string dialogueText = parts[3];

                if (!allDialogue.ContainsKey(itemName))
                    allDialogue[itemName] = new Dictionary<string, Dictionary<string, string>>();

                if (!allDialogue[itemName].ContainsKey(action))
                    allDialogue[itemName][action] = new Dictionary<string, string>();

                allDialogue[itemName][action][id] = dialogueText;
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading dialogue file: " + e.Message);
        }

        return allDialogue;
    }

    private void DebugDialogueData()
    {
        Debug.Log("Dialogue Data Loaded:");
        foreach (var itemEntry in dialogueData)
        {
            Debug.Log($"Item: {itemEntry.Key}");
            foreach (var actionEntry in itemEntry.Value)
            {
                Debug.Log($"  Action: {actionEntry.Key}");
                foreach (var idEntry in actionEntry.Value)
                {
                    Debug.Log($"    ID: {idEntry.Key} => Dialogue: {idEntry.Value}");
                }
            }
        }
    }
}
