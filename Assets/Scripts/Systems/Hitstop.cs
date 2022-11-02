using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UI.Statistics;

public class Hitstop : MonoBehaviour
{
    private Health _h;
    [SerializeField]
    private float stopTime;
    void Start()
    {
        _h = GetComponent<PlayerHealth>().Health;
        _h.OnValueDecreased += StartHitStop;
    }

    private void StartHitStop()
    {
        StartCoroutine(Stop());
    }
    
    private IEnumerator Stop()
    {
        Time.timeScale = 0.001f;
        yield return new WaitForSeconds(stopTime * 0.001f);
        Time.timeScale = 1;
    }
    
    
}
