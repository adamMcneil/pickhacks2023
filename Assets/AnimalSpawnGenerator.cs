using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawnGenerator : MonoBehaviour
{

    public Terrain terrain;
    public GameObject rabbitObject;
    public GameObject foxObject;
    public float minY = 1.3f;
    public int numOfRabbitsToBeSpawned = 20;
    public int numOfFoxesToBeSpawned = 5;
  

    public void SpawnAnimals()
    {
        for (int i = 0; i < numOfRabbitsToBeSpawned; i++)
        {
            spawnRabbits();
        }
        for (int i = 0; i < numOfFoxesToBeSpawned; i++)
        {
            spawnFoxes();
        }
    }

    private void spawnRabbits()
    {
          // Get the terrain data
          TerrainData terrainData = terrain.terrainData;

          // Generate a random position on the terrain
          Vector3 position = new Vector3(Random.Range(0.0f, terrainData.size.x), 0.0f, Random.Range(0.0f, terrainData.size.z));

          // Get the height of the terrain at the position
          position.y = terrain.SampleHeight(position);

          // Check if the height is above the minimum Y level
          if (position.y >= minY)
          {
              // Spawn the object at the position
              GameObject spawnedObject = Instantiate(rabbitObject, position, Quaternion.identity);
          }
          else
          {
              // Try again if the height is below the minimum Y level
              spawnRabbits();
          }
    }
   private void spawnFoxes()
    {
        // Get the terrain data
        TerrainData terrainData = terrain.terrainData;

        // Generate a random position on the terrain
        Vector3 position = new Vector3(Random.Range(0.0f, terrainData.size.x), 0.0f, Random.Range(0.0f, terrainData.size.z));

        // Get the height of the terrain at the position
        position.y = terrain.SampleHeight(position);

        // Check if the height is above the minimum Y level
        if (position.y >= minY)
        {
            GameObject spawnedObject = Instantiate(foxObject, position, Quaternion.identity);
        }
        else
        {
            // Try again if the height is below the minimum Y level
            spawnFoxes();
        }
    }
}
