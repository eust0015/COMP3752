using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    private GameObject[] spawnLocation;
    public GameObject[] npcPrefab;
    public float spawnChance;
    public string locationName = "NPCSpawn";
    void Start()
    {
        NPCSpawnChance();
    }
        void NPCSpawnChance()
    {
        //generates a number to decide if the npc will spawn. If it does, it gets a list of all avaliable spawn logations and chooses a random one
        /*
         * public static int Range(int minInclusive, int maxExclusive);
         * maxExcusive is exclusive, so for example Random.Range(0, 10) will return a value between 0 and 9, each with approximately equal probability.
        */
        spawnLocation = GameObject.FindGameObjectsWithTag(locationName);
        for (int i = 0; i < spawnLocation.Length; i++)
        {
            if (Random.Range(0f, 100f) >= 100 - spawnChance)
            {
                Instantiate(npcPrefab[Random.Range(0, npcPrefab.Length)], new Vector3(spawnLocation[i].transform.position.x, spawnLocation[i].transform.position.y, -1), Quaternion.identity, transform);
            }
        }
    }
}
