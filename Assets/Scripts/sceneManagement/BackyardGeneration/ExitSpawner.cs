using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitSpawner : MonoBehaviour
{
    private GameObject Exit;
    private GameObject[] ExitsArray;
    public int spawnChance = 100;
    private float doorTimer = 3f;
    public string lastRoom = "Above";
    public Sprite door50;
    public Sprite door0;
    public Sprite door100;
    void Awake()
    {
        Exit = gameObject;
        ExitsArray = new GameObject[4];
        for (int i = 0; i < ExitsArray.Length; i++)
        {
            ExitsArray[i] = Exit.transform.GetChild(i).gameObject;
            ExitsArray[i].SetActive(false);
            if (spawnChance >= Random.Range(0, 100))
            {
                OpenDoor(i);
            }
        }
        EnsureExit();
        OpenDoor(FindLastRoom());
    }
    public void SetLastRoom(string prevRoom)
    {
        lastRoom = prevRoom;
        OpenDoor(FindLastRoom());
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

    public void EnsureExit()
    {
        bool noExits = true;
        int count = 0;
        for (int i = 0; i < ExitsArray.Length; i++)
        {
            if(ExitsArray[i].activeSelf)
            {
                count++;
            }
            if (count >= 2)
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
            OpenDoor(randomno);
        }
    }

    private void OpenDoor(int roomNo)
    {
        float x = doorTimer;
        ExitsArray[roomNo].SetActive(true);
        while(x >= 0)
        {
            x -= Time.deltaTime;
            if(x <= 0)
            {
                ExitsArray[roomNo].GetComponent<SpriteRenderer>().sprite = door100;
            }
            else if(x <= doorTimer/2)
            {
                ExitsArray[roomNo].GetComponent<SpriteRenderer>().sprite = door50;

            }
        }

    }

    public void closeAllDoors()
    {
        for (int i = 0; i < ExitsArray.Length; i++)
        {
            float x = doorTimer;
            while (x >= 0)
            {
                x -= Time.deltaTime;
                if (x <= 0)
                {
                    ExitsArray[i].GetComponent<SpriteRenderer>().sprite = door0;
                }
                else if (x <= doorTimer / 2)
                {
                    ExitsArray[i].GetComponent<SpriteRenderer>().sprite = door50;

                }
            }
            ExitsArray[i].SetActive(false);
        }
    }
}
