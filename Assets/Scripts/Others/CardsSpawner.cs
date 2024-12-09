using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain in the Inspector
    public GameObject[] objectsToSpawn; // Assign 3 prefabs here
    public float spawnRadius = 10f; // Radius for spawning
    public int numberOfObjects = 10; // Number of objects to spawn
    public float avoidRadius = 2f; // Minimum distance between objects
    public float destroyDelay = 5f; // Time before destroying the spawned objects

    private System.Collections.Generic.List<GameObject> spawnedObjects = new System.Collections.Generic.List<GameObject>();
    private int spawnCounter = 0; // Counter for unique naming

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition;

            // Try to find a valid position
            int attempts = 0;
            do
            {
                randomPosition = GetRandomPosition();
                float terrainHeight = terrain.SampleHeight(randomPosition);
                randomPosition.y = terrainHeight + terrain.transform.position.y;
                attempts++;
            } while (IsPositionNearOtherObjects(randomPosition) && attempts < 100);

            // Spawn object if a valid position is found
            if (attempts < 100)
            {
                GameObject prefabToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
                GameObject spawnedObject = Instantiate(prefabToSpawn, randomPosition, prefabToSpawn.transform.rotation);

                // Assign a unique name to the spawned object
                spawnedObject.name = $"{prefabToSpawn.name}_{spawnCounter}";
                spawnCounter++;

                spawnedObjects.Add(spawnedObject);

                // Schedule destruction after delay
                StartCoroutine(DestroyAfterDelay(spawnedObject, destroyDelay));
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 center = transform.position;
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(0f, spawnRadius);
        float x = center.x + Mathf.Cos(angle) * distance;
        float z = center.z + Mathf.Sin(angle) * distance;

        return new Vector3(x, 0, z); // Y is set to 0, will adjust later
    }

    bool IsPositionNearOtherObjects(Vector3 position)
    {
        foreach (GameObject obj in spawnedObjects)
        {
            if (Vector3.Distance(obj.transform.position, position) < avoidRadius)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the spawn radius in the Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    public void DestroyObjectByName(string objectName)
    {
        GameObject objectToDestroy = spawnedObjects.Find(obj => obj.name == objectName);
        if (objectToDestroy != null)
        {
            spawnedObjects.Remove(objectToDestroy); // Remove from the list
            Destroy(objectToDestroy); // Destroy the GameObject
            Debug.Log($"Object '{objectName}' has been destroyed.");
        }
        else
        {
            Debug.LogWarning($"Object with name '{objectName}' not found.");
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spawnedObjects.Contains(obj))
        {
            spawnedObjects.Remove(obj);
            Destroy(obj);
        }
    }
}
