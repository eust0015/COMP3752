using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterNewRoom : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject backyardManager = GameObject.FindGameObjectsWithTag("GameController")[0];
        backyardManager.GetComponent<YardManager>().LoadNewRoom(gameObject.name);
        Debug.Log(gameObject.name);
    }
}
