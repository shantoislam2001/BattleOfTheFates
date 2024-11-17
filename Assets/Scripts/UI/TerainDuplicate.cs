using UnityEngine;

public class TerrainDuplicator : MonoBehaviour
{
    public Terrain originalTerrain;

    void Start()
    {
        // মূল টেরেইন থেকে নতুন টেরেইন ডেটা কপি করা
        TerrainData clonedTerrainData = new TerrainData();
        clonedTerrainData.heightmapResolution = originalTerrain.terrainData.heightmapResolution;
        clonedTerrainData.size = originalTerrain.terrainData.size;

        // উচ্চতা এবং ডিটেইল ডেটা কপি করা
        clonedTerrainData.SetHeights(0, 0, originalTerrain.terrainData.GetHeights(0, 0, originalTerrain.terrainData.heightmapResolution, originalTerrain.terrainData.heightmapResolution));
        clonedTerrainData.treeInstances = new TreeInstance[0]; // গাছ রিমুভ করা হচ্ছে

        // মূল টেরেইনের splatPrototypes কপি করা
        clonedTerrainData.splatPrototypes = originalTerrain.terrainData.splatPrototypes;

        // নতুন টেরেইন তৈরি এবং সেটিংস প্রয়োগ করা
        GameObject newTerrainObject = Terrain.CreateTerrainGameObject(clonedTerrainData);
        newTerrainObject.transform.position = originalTerrain.transform.position;
        newTerrainObject.name = "ClonedTerrainWithoutTrees";

        // মিনি ম্যাপ লেয়ার সেট করা
        int miniMapLayer = LayerMask.NameToLayer("Minimap");
        if (miniMapLayer != -1)
        {
            newTerrainObject.layer = miniMapLayer;
        }
        else
        {
            Debug.LogWarning("Layer 'minimap' not found. Please create it in the Layer settings.");
        }
    }
}
