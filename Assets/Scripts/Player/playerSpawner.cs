using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    //This script is used on an object to choose where to spawn the player
    
    public GameObject spawnLocation;
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        player.transform.position = spawnLocation.transform.position;
        //Instantiate(playerPrefab, new Vector3(spawnLocation.transform.position.x, spawnLocation.transform.position.y, -5), Quaternion.identity);
    }
}
