using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private EnemyHealth h;
    
    public void Hit(int damage)
    {
        h.Health.DecreaseValue(damage);
        if (h.Health.Value <= 0)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator HitAnimation()
    {
        yield return null;
    }

    private IEnumerator Death()
    {
        yield return null;
    }
}
