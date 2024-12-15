using System.Collections.Generic;
using UnityEngine;
using System;

public class AIManager : MonoBehaviour
{
    private Dictionary<string, AICharacter> aiCharacters;

    void Start()
    {
    }

    // Set a target for a specific AI by name
    public void SetTargetForAI(string characterName, Vector3 target)
    {
      
    }

    // Clear the target for a specific AI by name
    public void ClearTargetForAI(string characterName)
    {
        
    }

    // Make a specific AI run by name
   

    // Set a target and a callback for a specific AI by name
    public void SetTargetForAIWithCallback(string characterName, Vector3 target, Action callback)
    {
       
    }


}