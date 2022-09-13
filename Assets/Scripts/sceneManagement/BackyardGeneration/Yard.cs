using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yard : MonoBehaviour
{
    private GameObject roomAbove = null;
    private GameObject roomBelow = null;
    private GameObject roomLeft = null;
    private GameObject roomRight = null;

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
                break;

        }
    }
}
