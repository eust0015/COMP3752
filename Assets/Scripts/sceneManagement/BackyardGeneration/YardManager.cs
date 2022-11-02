using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity; //FMOD

public class YardManager : MonoBehaviour
{
    public List<GameObject> roomPrefabs;
    public List<GameObject> createdRooms = new List<GameObject>();
    public int currentRoom = 0;

    //Yarny FMOD: Whatever you put in this block will show up as sound file browsing options under this script name in Unity
    //You can use these variables in scripts. You can change the actual mp3/sound file (in dropdown unity browser) anytime you like.
    // The choices for the actual sound files to bind to these variables depends what you have setup in the FMOD music app itself.
    [SerializeField]
    // public FMODUnity.EventReference shootFMSound;
    // public FMODUnity.EventReference strikeFMSound;
    // public FMODUnity.EventReference gameOverFMSound;
    // public FMODUnity.EventReference royalSound;

    //Yarny FMOD: Instances inc. Parameters. In START (Awake) the music gets linked to your instance.
    public FMOD.Studio.EventInstance instance;
    public FMODUnity.EventReference fmodEvent; //music will be your music track & variables to control the music

    //Yarny FMOD: This is where you set the range of variables that will affect the music

    // [SerializeField]
    // [Range(0f, 6F)]
    // public float playerHealthPR;

    // [SerializeField]
    // [Range(0f, 10F)]
    // public float currentNumberOfEnemies;

    void Awake()
    {
        //GameManager.current.onTimerComplete += spawnBoss;

        currentRoom = 0;
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            roomPrefabs[i].SetActive(true);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Above", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Below", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Left", -1);
            roomPrefabs[i].GetComponent<Yard>().setRoom("Right", -1);
        }
        roomPrefabs[0].GetComponent<Yard>().setRoom("Above", -2);

        createdRooms.Add(Instantiate(roomPrefabs[0], new Vector3(0, 0, 0), Quaternion.identity));

        //Yarny FMOD: Set an instance of the music (the music event) and start it
        //playerHealthPR = 6.0f;
        //currentNumberOfEnemies = 0.0f;
        instance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent); // This also works (like tutorial, older style)
        instance.start();


        //Yarny FMOD: Examples of how to set variables
        // instance.setParameterByName("playerHealthPR", 2.0f); //This will set the underlying FMOD to the value chosen, but won't show un unity slider UI
        // instance.setParameterByName("currentNumberOfEnemies", 3.0f);

        //This binds them. I use it in my update script. You will see the UI slider change, and the underlying FMOD will change.
        //instance.setParameterByName("playerHealthPR", playerHealthPR);
        //instance.setParameterByName("currentNumberOfEnemies", currentNumberOfEnemies);

    }

    public void LoadNewRoom(string roomLocation)
    {
        // var RndB = new System.Random();
        // var StrB = RndB.Next(1, 10);
        string previousRoom = "Below";
        switch (roomLocation)
        {
            case "Above":
                previousRoom = "Below";
                //currentNumberOfEnemies = StrB; //Yarny FMOD: Variable changes depending on room
                break;
            case "Below":
                previousRoom = "Above";
                //currentNumberOfEnemies = StrB;
                break;
            case "Left":
                previousRoom = "Right";
                //currentNumberOfEnemies = StrB;
                break;
            case "Right":
                previousRoom = "Left";
                //currentNumberOfEnemies = StrB;
                break;
            default:
                break;
        }

        //Yarny: FMOD - Apply those variable changes made above in room switch
        //instance.setParameterByName("playerHealthPR", playerHealthPR);
        //instance.setParameterByName("currentNumberOfEnemies", currentNumberOfEnemies);

        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<playerMovement>().TeleportPlayer(roomLocation);
        //Enters this if statment if there is no room on the other side of the door
        if (createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation) == -1)
        {
            createdRooms[currentRoom].SetActive(false);
            //Checks if a room is going to be spawned on another room. If so, it links the room instead
            if (!RoomOverlap(roomLocation))
            {
                //the creation of the new room
                createdRooms.Add(Instantiate(roomPrefabs[Random.Range(1, roomPrefabs.Count)], new Vector3(0, 0, 0), Quaternion.identity));
                createdRooms[currentRoom].GetComponent<Yard>().setRoom(roomLocation, createdRooms.Count - 1);
                createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setRoom(previousRoom, currentRoom);
                createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("x", createdRooms[currentRoom].GetComponent<Yard>().getCoords("x"));
                createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("y", createdRooms[currentRoom].GetComponent<Yard>().getCoords("y"));
                //writes the coordinates of the new room onto the new room
                switch (roomLocation)
                {
                    case "Above":
                        createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("y", createdRooms[createdRooms.Count - 1].GetComponent<Yard>().getCoords("y") + 1);
                        break;
                    case "Below":
                        createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("y", createdRooms[createdRooms.Count - 1].GetComponent<Yard>().getCoords("y") - 1);
                        break;
                    case "Left":
                        createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("x", createdRooms[createdRooms.Count - 1].GetComponent<Yard>().getCoords("x") - 1);
                        break;
                    case "Right":
                        createdRooms[createdRooms.Count - 1].GetComponent<Yard>().setCoords("x", createdRooms[createdRooms.Count - 1].GetComponent<Yard>().getCoords("x") + 1);
                        break;
                }
                currentRoom = createdRooms.Count - 1;
            }
            //The linking of the current room to the one next to it
            else
            {
                createdRooms[createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation)].SetActive(true);
                createdRooms[currentRoom].SetActive(false);
                currentRoom = createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation);
                createdRooms[currentRoom].GetComponentInChildren<ExitSpawner>().SetLastRoom(previousRoom);
            }
        }
        //Statement below loads the beehive when entered
        else if (createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation) == -2)
        {
            //Yarny: FMOD - Stop Main Music (It doubles up otherwise later) and play a royal one shot
            instance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); //Player is dead, stop music
            //RuntimeManager.PlayOneShot(royalSound, gameObject.transform.position);
            SceneManager.LoadScene("beeHive");
        }
        //Unloads the old room and loads the new room
        else
        {
            createdRooms[createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation)].SetActive(true);
            createdRooms[currentRoom].SetActive(false);
            currentRoom = createdRooms[currentRoom].GetComponent<Yard>().getRoom(roomLocation);
        }
    }

    private bool RoomOverlap(string roomLocation)
    {
        for (int i = 0; i < createdRooms.Count; i++)
        {
            switch (roomLocation)
            {
                case "Above":
                    if (createdRooms[i].GetComponent<Yard>().getCoords("y") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("y") + 1)
                    {
                        if (createdRooms[i].GetComponent<Yard>().getCoords("x") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("x"))
                        {
                            linkRooms(currentRoom, i, roomLocation);
                            return true;
                        }
                    }
                    break;
                case "Below":
                    if (createdRooms[i].GetComponent<Yard>().getCoords("y") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("y") - 1)
                    {
                        if (createdRooms[i].GetComponent<Yard>().getCoords("x") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("x"))
                        {
                            linkRooms(currentRoom, i, roomLocation);
                            return true;
                        }
                    }
                    break;
                case "Left":
                    if (createdRooms[i].GetComponent<Yard>().getCoords("x") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("x") - 1)
                    {
                        if (createdRooms[i].GetComponent<Yard>().getCoords("y") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("y"))
                        {
                            linkRooms(currentRoom, i, roomLocation);
                            return true;
                        }
                    }
                    break;
                case "Right":
                    if (createdRooms[i].GetComponent<Yard>().getCoords("x") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("x") + 1)
                    {
                        if (createdRooms[i].GetComponent<Yard>().getCoords("y") == createdRooms[currentRoom].GetComponent<Yard>().getCoords("y"))
                        {
                            linkRooms(currentRoom, i, roomLocation);
                            return true;
                        }
                    }
                    break;
            }
        }
        return false;
    }

    private void linkRooms(int roomPlayerIsIn, int roomPlayerWillBeIn, string direction)
    {
        createdRooms[roomPlayerIsIn].GetComponent<Yard>().setRoom(direction, roomPlayerWillBeIn);
    }

    public void spawnBoss()
    {
        createdRooms[currentRoom].GetComponentInChildren<ExitSpawner>().closeAllDoors();
    }
}
