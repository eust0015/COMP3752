using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Behaviour : MonoBehaviour
{
    protected Unit u;
    [SerializeField]
    protected float maxDistance = 5f;
    protected bool tracking;

    private void Awake()
    {
        u = GetComponent<Unit>();
        tracking = false;
        StartCoroutine(Condition());
    }

    protected virtual IEnumerator Condition()
    {
        while (true)
        {
            u.StartPath();
            tracking = true;
            yield return null;
        }
    }

    protected virtual IEnumerator OnDeath()
    {
        Destroy( this);
        yield return null;
    }
}
