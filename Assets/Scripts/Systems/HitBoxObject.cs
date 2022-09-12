using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using Unity.VisualScripting;
using UnityEngine;


public class HitBoxObject : MonoBehaviour
{
    public AttackController owner;
    
    public float timer;
    public int damage;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        if (other.gameObject == owner.gameObject) return;
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            var otherHealth = other.gameObject.GetComponent<EnemyHealth>();
            owner.RequestAttack(otherHealth.Health, damage);
        }
        else if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            var otherHealth = other.gameObject.GetComponent<PlayerHealth>().Health;
            Debug.Log(other.gameObject.GetComponent<PlayerHealth>());
            Debug.Log(otherHealth);
            owner.RequestAttack(otherHealth, damage);
        }
    }
}
