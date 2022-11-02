using Enemy;
using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using UnityEngine;
using Audio;

public class EnemyDamaged : MonoBehaviour
{
     [SerializeField] private EnemyHealth health;
    [SerializeField] private FMODAudioSource damageSound;
    private void Start()
    {
        health.Health.OnValueDecreased += Damaged;
    }

    private void Damaged()
    {
 
        damageSound.PlaySound();
    }

    private void OnDestroy()
    {
        health.Health.OnValueDecreased -= Damaged;
    }
}
