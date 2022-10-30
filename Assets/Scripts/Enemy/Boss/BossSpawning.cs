using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossSpawning : MonoBehaviour
{
    public GameObject boss;

    void Start()
    {
        //GameManager.current.onTimerComplete += SpawnBoss;
    }

    void Update()
    {
    }

    void SpawnBoss()
    {
        GameObject[] rooms = GameObject.FindGameObjectsWithTag("Backyard");
        for(int i = 0; i < rooms.Length; i++)
        {
            rooms[i].GetComponentInChildren<ExitSpawner>().closeAllDoors();
        }
        Instantiate(boss, gameObject.transform.position, Quaternion.identity, transform);
    }

    private void OnDestroy()
    {
       // GameManager.current.onTimerComplete -= SpawnBoss;
    }
}
