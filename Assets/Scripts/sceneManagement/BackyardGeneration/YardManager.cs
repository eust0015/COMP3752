using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardManager : MonoBehaviour
{
    public GameObject startingRoom;
    public List<GameObject> roomPrefabs;
    private GameObject currentRoom;
    private GameObject[] exits;
    void Awake()
    {
        Instantiate(startingRoom, new Vector3(0,0,0), Quaternion.identity);
        currentRoom = startingRoom;
    }

    public void LoadNewRoom(string roomLocation)
    {
        string previousRoom = "Below";
        switch(roomLocation)
        {
            case "Above":
                previousRoom = "Below";
                break;
            case "Below":
                previousRoom = "Above";
                break;
            case "Left":
                previousRoom = "Right";
                break;
            case "Right":
                previousRoom = "Left";
                break;
            default:
                break;
        }
        if(currentRoom.GetComponent<Yard>().getRoom(roomLocation) == null)
        {
            GameObject newRoom = roomPrefabs[Random.Range(0, roomPrefabs.Count)];
            currentRoom.GetComponent<Yard>().setRoom(roomLocation, newRoom);
            Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity);
            newRoom.GetComponent<Yard>().setRoom(previousRoom, currentRoom);
            currentRoom.SetActive(false);
            currentRoom = newRoom;
        }
        else
        {
            Debug.Log("Testing2");
            currentRoom = currentRoom.GetComponent<Yard>().getRoom(roomLocation);
        }
        GameObject.FindGameObjectWithTag("Backyard").GetComponentInChildren<ExitSpawner>().lastRoom = previousRoom;

    }
}
