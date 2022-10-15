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
    public bool basedOnTag = false;
    public string _tag;
    public float timer;
    public int damage;
    public bool showHitbox = false;

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit " + other.name);
        if (basedOnTag)
        {
            if(!other.CompareTag(_tag)) return;
        }
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

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        if (showHitbox) Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
    }
}
