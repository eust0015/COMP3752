using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yard : MonoBehaviour
{
    public GameObject startingRoom;
    private GameObject currentRoom;
    private GameObject[] exits;
    void Awake()
    {
        Instantiate(startingRoom, new Vector3(0,0,0), Quaternion.identity);
        currentRoom = startingRoom;
    }

    void Update()
    {
        
    }

    void LoadNewRoom()
    {
        
    }
}
