using UnityEngine;

public class CardsSpawner : MonoBehaviour
{
    public Terrain terrain; 
    public GameObject[] objectsToSpawn; 
    public float spawnRadius = 10f;
    public int numberOfObjects = 10;
    public float avoidRadius = 2f; 
    public float destroyDelay = 5f; 

    private System.Collections.Generic.List<GameObject> spawnedObjects = new System.Collections.Generic.List<GameObject>();

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
                spawnedObjects.Add(spawnedObject);
                Destroy(spawnedObject, destroyDelay);
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
}
