using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected Unit u;
    [SerializeField]
    protected float maxDistance = 5f;
    protected bool tracking;

    protected bool atkCon;
    protected float atkCD;

    protected AttackController _a;

    private void Awake()
    {
        _a = GetComponent<AttackController>();
        u = GetComponent<Unit>();
        tracking = false;
        StartCoroutine(Condition());

        GetComponent<EnemyHealth>().Health.OnValueZero += Death;
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

    protected virtual void Death()
    {
        StartCoroutine(OnDeath());
    }
    
    protected virtual IEnumerator OnDeath()
    {
        Destroy( gameObject);
        yield return null;
    }

    protected virtual void Hit()
    {
        StartCoroutine(OnHit());
    }

    protected virtual IEnumerator OnHit()
    {
        
        yield return null;
    }

    protected virtual IEnumerator Attack()
    {
        if(_a == null) yield break;
    }
}
