using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private GameObject map;
    private Vector3 location;
    private int mapWidth = 26;
    private int mapHeight = 14;

    void Update()
    {
        //finds the player game object and focuses the camera on it
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        //map = GameObject.FindGameObjectsWithTag("Backyard")[GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<YardManager>().currentRoom];

        if (player.transform.position.x > mapWidth/2)
        {
            location.x = mapWidth / 2;
        }
        else if (player.transform.position.x < -mapWidth / 2)
        {
            location.x = -mapWidth / 2;
        }
        else
        {
            location.x = player.transform.position.x;
        }
        if (player.transform.position.y > mapHeight / 2)
        {
            location.y = mapHeight / 2;
        }
        else if (player.transform.position.y < -mapHeight / 2)
        {
            location.y = -mapHeight / 2;
        }
        else
        {
            location.y = player.transform.position.y;
        }

        location.z = -10;

        transform.position = location;
        //Debug.Log(location.position.x + "," + location.position.y);
    }
}
