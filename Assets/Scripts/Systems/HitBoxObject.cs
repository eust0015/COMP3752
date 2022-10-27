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
    public float angle;
    public float knockback;
    public float momentum;

    private void Start()
    {
        transform.RotateAround(owner.transform.position, Vector3.back, angle);
    }

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
            if (owner.CompareTag("Player"))
            {
                owner.Hit();
                if (otherHealth.Health.Value - damage <= 0)
                {
                    owner.Kill();
                }
            }
            owner.RequestAttack(otherHealth.Health, damage);
            var m = (new Vector2(other.transform.position.x - transform.position.x,
                -(other.transform.position.y - transform.position.y))).normalized * knockback * 3;
            other.attachedRigidbody.velocity += m;
            
            
        }
        else if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            var otherHealth = other.gameObject.GetComponent<PlayerHealth>().Health;
            owner.RequestAttack(otherHealth, damage);
        }
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.DrawWireCube(transform.position, transform.localScale);
        if (showHitbox) Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
    }
}
