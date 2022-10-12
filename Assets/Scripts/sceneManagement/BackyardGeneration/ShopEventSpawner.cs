using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEventSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject shopPrefab;
    public GameObject eventPrefab;
    public float spawnChance = 30;
    void Start()
    {
        if (spawnChance >= Random.Range(0, 100))
        {
            Instantiate(eventPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity, transform);
        }
        else if (spawnChance >= Random.Range(0, 100))
        {
            Instantiate(shopPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity, transform);
        }
    }
}
