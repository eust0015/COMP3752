using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSpawner : MonoBehaviour
{
    public List<GameObject> flowerPrefabs = new List<GameObject>();
    public float spawnChance;
    public int spawnRadius = 3;
    void Start()
    {
        float xval = gameObject.transform.position.x + Random.Range(-spawnRadius, spawnRadius);
        float yval = gameObject.transform.position.y + Random.Range(-spawnRadius, spawnRadius);
        if (xval >= 28)
        {
            xval = 27;
        }
        else if (xval <= -28)
        {
            xval = -27;
        }
        if (yval >= 15)
        {
            yval = 14;
        }
        else if (yval <= -15)
        {
            yval = -14;
        }
        Instantiate(flowerPrefabs[Random.Range(0,flowerPrefabs.Count)], new Vector3(xval, yval, -1), Quaternion.identity, transform);
    }
}
