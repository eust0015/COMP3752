using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject player;
    private Transform location;

    void Update()
    {
        //finds the player game object and focuses the camera on it
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        location = player.transform;
        transform.position = new Vector3(location.position.x,location.position.y, -10);
        //Debug.Log(location.position.x + "," + location.position.y);
    }
}
