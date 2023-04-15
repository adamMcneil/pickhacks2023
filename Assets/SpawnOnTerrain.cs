using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnTerrain : MonoBehaviour
{
    public Terrain terrain;
    public GameObject objectToSpawn;
    public float minY = 0.0f;
    public float spawnInterval = 5.0f;
    public int numberToSpawn = 5;
    public int maxBushes = 50;


    public void Stop()
    {
        StopAllCoroutines();
    }

    public void SpawnPlants()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            SpawnObjectOnTerrain();
        }
        StartCoroutine(SpawnObjectsOnTimer());

    }

    private IEnumerator SpawnObjectsOnTimer()
    {
        while (true)
        {
            if (Helpers.bushes.Count < maxBushes)
            {
              SpawnObjectOnTerrain();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObjectOnTerrain()
    {
      TerrainData terrainData = terrain.terrainData;
      Vector3 position = new Vector3(Random.Range(0.0f, terrainData.size.x), 0.0f, Random.Range(0.0f, terrainData.size.z));
      position.y = terrain.SampleHeight(position);
      if (position.y >= minY)
      {
          Instantiate(objectToSpawn, position, Quaternion.identity);
      }
    }
}
