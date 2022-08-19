using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    public GameObject spawnLocation;
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, -5), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
