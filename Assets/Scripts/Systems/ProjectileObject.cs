using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;
using Player;
using UI.Statistics;

public class ProjectileObject : MonoBehaviour
{
    public float speed;
    public int damage;
    public AttackController owner;
    private Rigidbody2D rb2d;

    public bool pierce = false;
    public int pierceCount = -1;
    public bool basedOnTag = false;
    public string _tag;
    public float timer;

    public Projectile p;

    private void Start()
    {
        rb2d = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var _t = transform;
        rb2d.velocity = _t.up * speed;
        timer -= Time.deltaTime;
        if (timer <= 0 || owner == null) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Ranged Hit " + other.name);
        if (basedOnTag)
        {
            if(!other.CompareTag(_tag)) return;
        }
        if (other.gameObject == owner.gameObject) return;
        
        if (other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            Debug.Log("AttackEnemy");
            var otherHealth = other.gameObject.GetComponent<EnemyHealth>();
            Attack(otherHealth.Health, damage);
        }
        else if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            var otherHealth = other.gameObject.GetComponent<PlayerHealth>().Health;
            Attack(otherHealth, damage);
        }  
    }

    private void Attack(Health h, int d)
    {
        owner.RequestAttack(h, d);
        pierceCount--;
        if (!pierce || pierceCount == 0) Destroy(gameObject);
    }
}
