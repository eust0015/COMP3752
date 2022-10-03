using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] spawnLocations;
    public GameObject enemyPrefab;
    public float spawnChance;
    public string locationName = "NPCSpawn";
    void Start()
    {
        NPCSpawnChance();
    }
    void NPCSpawnChance()
    {
        spawnLocations = GameObject.FindGameObjectsWithTag(locationName);
        for (int i = 0; i < spawnLocations.Length; i++)
        {
            if (spawnChance >= Random.Range(0, 100))
            {
                Instantiate(enemyPrefab, new Vector3(spawnLocations[i].transform.position.x, spawnLocations[i].transform.position.y, -1), Quaternion.identity, transform);
            }
        }
        
    }
}
