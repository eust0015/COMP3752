using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnChance;
    public int spawnRadius = 5;
    public int maxEnemies = 5;
    void Start()
    {
        for(int i = 0; i < maxEnemies; i++)
        {
            if(spawnChance >= Random.Range(0,100))
            {
                float xval = gameObject.transform.position.x + Random.Range(-spawnRadius, spawnRadius);
                float yval = gameObject.transform.position.y + Random.Range(-spawnRadius, spawnRadius);
                if(xval >= 28)
                {
                    xval = 27;
                }
                else if(xval <= -28)
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
                Instantiate(enemyPrefab, new Vector3(xval, yval, -1), Quaternion.identity, transform);
            }
        }
    }
}
