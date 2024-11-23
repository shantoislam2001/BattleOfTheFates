using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    private Dictionary<string, AICharacter> aiCharacters;

    void Start()
    {
        // Find all AICharacter components in the scene
        AICharacter[] allCharacters = FindObjectsOfType<AICharacter>();
        aiCharacters = new Dictionary<string, AICharacter>();

        // Add them to the dictionary using their GameObject name as the key
        foreach (AICharacter character in allCharacters)
        {
            aiCharacters[character.GetCharacterName()] = character;
        }
    }

    // Set a target for a specific AI by name
    public void SetTargetForAI(string characterName, Vector3 target)
    {
        if (aiCharacters.TryGetValue(characterName, out AICharacter character))
        {
            character.SetTarget(target);
        }
        else
        {
            Debug.LogWarning($"AICharacter with name '{characterName}' not found.");
        }
    }

    // Clear the target for a specific AI by name
    public void ClearTargetForAI(string characterName)
    {
        if (aiCharacters.TryGetValue(characterName, out AICharacter character))
        {
            character.ClearTarget();
        }
        else
        {
            Debug.LogWarning($"AICharacter with name '{characterName}' not found.");
        }
    }

    // Make a specific AI run by name
    public void MakeAIRun(string characterName)
    {
        if (aiCharacters.TryGetValue(characterName, out AICharacter character))
        {
            character.StartRunning();
        }
        else
        {
            Debug.LogWarning($"AICharacter with name '{characterName}' not found.");
        }
    }

    // Make a specific AI walk by name
    public void MakeAIWalk(string characterName)
    {
        if (aiCharacters.TryGetValue(characterName, out AICharacter character))
        {
            character.StopRunning();
        }
        else
        {
            Debug.LogWarning($"AICharacter with name '{characterName}' not found.");
        }
    }
}