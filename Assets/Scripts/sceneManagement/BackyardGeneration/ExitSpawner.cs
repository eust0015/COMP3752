using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    private GameObject Exit;
    private GameObject[] ExitsArray;
    public int spawnChance = 100;
    public string lastRoom = "Below";
    void Awake()
    {
        Exit = gameObject;
        ExitsArray = new GameObject[4];
        for (int i = 0; i < ExitsArray.Length; i++)
        {
            ExitsArray[i] = Exit.transform.GetChild(i).gameObject;
        }

        for (int i = 0; i < ExitsArray.Length; i++)
        {
            ExitsArray[i].SetActive(false);
        }

        for (int i = 0; i < ExitsArray.Length; i++)
        {
            if (spawnChance >= Random.Range(0, 100))
            {
                ExitsArray[i].SetActive(true);
            }
        }
        ExitsArray[FindLastRoom()].SetActive(true);
    }
    public void setLastRoom(string prevRoom)
    {
        lastRoom = prevRoom;
        ExitsArray[FindLastRoom()].SetActive(true);
    }

    private int FindLastRoom()
    {
        switch(lastRoom)
        {
            case "Above":
                return 0;
            case "Below":
                return 1;
            case "Right":
                return 2;
            case "Left":
                return 3;
            default:
                return 0;

        }
    }

    public void ensureExit()
    {
        bool noExits = true;
        for (int i = 0; i < ExitsArray.Length; i++)
        {
            if(i != FindLastRoom() && ExitsArray[i].activeSelf)
            {
                noExits = false;
            }
        }
        if(noExits)
        {
            int randomno = FindLastRoom();
            while(randomno == FindLastRoom())
            {
                randomno = Random.Range(0, ExitsArray.Length-1);
            }
            ExitsArray[randomno].SetActive(true);
        }
    }
}
