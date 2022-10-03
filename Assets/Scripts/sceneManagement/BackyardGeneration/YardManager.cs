using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public List<GameObject> createdRooms = new List<GameObject>();
    private int currentRoom = 0;
    private GameObject newRoom;
    private GameObject[] exits;
    void Awake()
    {
        currentRoom = 0;
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            roomPrefabs[i].SetActive(true);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Above", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Below", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Left", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Right", -1);
        }
        
        createdRooms.Add(Instantiate(roomPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity));
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

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<playerMovement>().TeleportPlayer(roomLocation);

        if (createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation) == -1)
        {
            createdRooms[currentRoom].SetActive(false);
            createdRooms.Add(Instantiate(roomPrefabs[Random.Range(1, roomPrefabs.Count - 1)], new Vector3(0, 0, 0), Quaternion.identity));
            createdRooms[currentRoom].GetComponent<Yard>().setRoom(roomLocation, createdRooms.Count-1);
            createdRooms[createdRooms.Count-1].GetComponent<Yard>().setRoom(previousRoom, currentRoom);
            currentRoom = createdRooms.Count-1;
        }
        else
        {
            createdRooms[createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation)].SetActive(true);
            createdRooms[currentRoom].SetActive(false);
            currentRoom = createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation);
        }
        //GameObject.FindGameObjectWithTag("Backyard").GetComponentInChildren<ExitSpawner>().lastRoom = previousRoom;

    }
}
