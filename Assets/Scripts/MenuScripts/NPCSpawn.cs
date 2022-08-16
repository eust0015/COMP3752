using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject npcPrefab;
    public float spawnChance;
    void Start()
    {
        NPCSpawnChance();
    }

    void Update()
    {
        
    }

    void NPCSpawnChance()
    {
        if(Random.Range(0f,100f) >= 100 - spawnChance)
        {
            Instantiate(npcPrefab, new Vector2(spawnLocation.transform.position.x, spawnLocation.transform.position.y), Quaternion.identity);
        }
    }
}
