using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSpawner : MonoBehaviour
{
    private GameObject Exit;
    private GameObject[] ExitsArray;
    public int spawnChance = 100;
    void Awake()
    {
        Exit = gameObject;
        ExitsArray = new GameObject[4];
        for (int i = 0; i < 3; i++)
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
                Debug.Log("view " + ExitsArray[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
