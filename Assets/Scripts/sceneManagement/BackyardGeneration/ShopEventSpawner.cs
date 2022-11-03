using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEventSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] shopPrefab;
    public GameObject[] eventPrefab;
    public float spawnChance = 30;
    void Start()
    {
        /*
         * public static int Range(int minInclusive, int maxExclusive);
         * maxExcusive is exclusive, so for example Random.Range(0, 10) will return a value between 0 and 9, each with approximately equal probability.
        */
        if (spawnChance >= Random.Range(0, 100))
        {
            Instantiate(eventPrefab[Random.Range(0, eventPrefab.Length)], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity, transform);
        }
        else if (spawnChance >= Random.Range(0, 100))
        {
            Instantiate(shopPrefab[Random.Range(0, shopPrefab.Length)], new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity, transform);
        }
    }
}
