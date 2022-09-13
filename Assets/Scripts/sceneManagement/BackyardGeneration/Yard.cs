using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yard : MonoBehaviour
{
    public GameObject roomAbove = null;
    public GameObject roomBelow = null;
    public GameObject roomLeft = null;
    public GameObject roomRight = null;

    public GameObject getRoom(string location)
    {
        switch(location)
        {
            case "Above":
                return roomAbove;
            case "Below":
                return roomBelow;
            case "Left":
                return roomLeft;
            case "Right":
                return roomRight;
            default:
                Debug.LogError("Looking for details of invalid location");
                return null;

        }
    }
    public void setRoom(string location, GameObject room)
    {
        switch (location)
        {
            case "Above":
                roomAbove = room;
                break;
            case "Below":
                roomBelow = room;
                break;
            case "Left":
                roomLeft = room;
                break;
            case "Right":
                roomRight = room;
                break;
            default:
                Debug.LogError("Invalid Location to set room");
                break;

        }
    }
}
