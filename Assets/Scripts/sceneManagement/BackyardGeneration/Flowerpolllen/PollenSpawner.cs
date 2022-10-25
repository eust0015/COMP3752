using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PollenSpawner : MonoBehaviour
{
    public float spawnTimer = 30;
    private float resetTime;
    public GameObject pollen;
    public int pollenVelocity = 5;
    private int i = 0;
    public List<GameObject> pollenList;
    void Start()
    {
        resetTime = spawnTimer;
        spawnTimer = 0;
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            pollenList.Add(Instantiate(pollen, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1), Quaternion.identity, transform));
            pollenList[i].GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-pollenVelocity,pollenVelocity), Random.Range(-pollenVelocity, pollenVelocity));
            i += 1;
            spawnTimer = resetTime;
        }
    }
}
