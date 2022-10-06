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
        FindLastRoom();
    }


    void FindLastRoom()
    {
        switch(lastRoom)
        {
            case "Above":
                ExitsArray[1].SetActive(true);
                break;
            case "Below":
                ExitsArray[2].SetActive(true);
                break;
            case "Right":
                ExitsArray[3].SetActive(true);
                break;
            case "Left":
                ExitsArray[4].SetActive(true);
                break;

        }
    }
}
