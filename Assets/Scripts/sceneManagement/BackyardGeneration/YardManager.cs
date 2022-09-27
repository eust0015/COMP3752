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
        startingRoom.SetActive(true);
        currentRoom = roomPrefabs[0];
        Instantiate(currentRoom, new Vector3(0, 0, 0), Quaternion.identity);
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
            currentRoom.SetActive(false);
            GameObject newRoom = roomPrefabs[Random.Range(1, roomPrefabs.Count)];
            newRoom.SetActive(true);
            currentRoom.GetComponent<Yard>().setRoom(roomLocation, newRoom);
            Instantiate(newRoom, new Vector3(0, 0, 0), Quaternion.identity);
            newRoom.GetComponent<Yard>().setRoom(previousRoom, currentRoom);
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
