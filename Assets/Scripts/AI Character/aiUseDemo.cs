using UnityEngine;

public class AIControlExample : MonoBehaviour
{
    public AIManager aiManager;

    void Start()
    {
        // Example: Set target for an AI named "AICharacter1"
        aiManager.SetTargetForAI("AICharacter1", new Vector3(10, 0, 10));

        // Make "AICharacter2" run
      
    }

  

}
