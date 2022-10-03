using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yard : MonoBehaviour
{
    public int roomAbove;
    public int roomBelow;
    public int roomLeft;
    public int roomRight;
    public int xValue;
    public int yValue;

    public int getCoords(string axis)
    {
        switch (axis)
        {
            case "x":
                return xValue;
            case "y":
                return yValue;
            default:
                Debug.LogError("Looking for details of invalid location");
                return 0;
        }
    }
    public void setCoords(string axis, int value)
    {
        switch (axis)
        {
            case "x":
                xValue = value;
                break;
            case "y":
                yValue = value;
                break;
            default:
                Debug.LogError("Looking for details of invalid location");
                break;
        }
    }

    public int getRoom(string location)
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
                return 0;

        }
    }
    public void setRoom(string location, int room)
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
