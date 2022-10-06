using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    //This script is used on an object to choose where to spawn the player
    
    public GameObject spawnLocation;
    public GameObject playerPrefab;
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, -5), Quaternion.identity);
    }
}
