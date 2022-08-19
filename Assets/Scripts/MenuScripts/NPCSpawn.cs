using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    private GameObject spawnLocation;
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
            spawnLocation = GameObject.FindGameObjectsWithTag("NPCSpawn")[Random.Range(0, GameObject.FindGameObjectsWithTag("NPCSpawn").Length)];
            Instantiate(npcPrefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, -1), Quaternion.identity);
        }
    }
}
