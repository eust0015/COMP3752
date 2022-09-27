using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YardManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    private List<GameObject> createdRooms = new List<GameObject>();
    private int currentRoom = 0;
    private GameObject newRoom;
    private GameObject[] exits;
    void Awake()
    {
        for(int i = 0; i < roomPrefabs.Count; i++)
        {
            roomPrefabs[i].SetActive(true);
        }
        createdRooms.Add(roomPrefabs[0]);
        Instantiate(createdRooms[0], new Vector3(0, 0, 0), Quaternion.identity);
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
        if(createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation) == 0)
        {
            createdRooms[currentRoom].SetActive(false);
            createdRooms.Add(roomPrefabs[Random.Range(1, roomPrefabs.Count-1)]);
            Instantiate(createdRooms[createdRooms.Count-1], new Vector3(0, 0, 0), Quaternion.identity);
            createdRooms[currentRoom].GetComponent<Yard>().setRoom(roomLocation, createdRooms.Count);
            createdRooms[createdRooms.Count-1].GetComponent<Yard>().setRoom(previousRoom, currentRoom);
            currentRoom += 1;
            Instantiate(createdRooms[currentRoom], new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            /*Debug.Log("Testing2");
            currentRoom = currentRoom.GetComponent<Yard>().getRoom(roomLocation);*/
        }
        //GameObject.FindGameObjectWithTag("Backyard").GetComponentInChildren<ExitSpawner>().lastRoom = previousRoom;

    }
}
