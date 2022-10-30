using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Player;
using Player.Relics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


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
            Random rnd = new Random();
            int atkMultiplier = 1;
            int critChance = 0;
            int heartStealChance = 0;
            
            // PlayerStats
            var playerStats = owner.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                critChance = (int)Math.Round(playerStats.critChance);
                atkMultiplier = (int)Math.Round(playerStats.atkMultiplier);
            }
            // --------------
            
            // Heart Stealer
            var heartStealer = owner.gameObject.GetComponent<PlayerHeartStealer>();
            if (heartStealer != null)
            {
                heartStealChance += heartStealer.HeartStealChance;
                if (heartStealChance >= rnd.Next(0, 101))
                {
                    heartStealer.DisplayHeartSteal(other.transform.position);
                    var playerHealth = owner.gameObject.GetComponent<PlayerHealth>().Health;
                    if (playerHealth != null)
                        playerHealth.IncreaseValue(heartStealer.HeartStealAmount);
                }
                    
            }
            // --------------

            // Adrenaline
            var adrenaline = owner.gameObject.GetComponent<PlayerAdrenaline>();
            if (adrenaline != null)
            {
                var playerHealth = owner.gameObject.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                    if (playerHealth.Health.Value < adrenaline.AmountHealthShouldBeBelow)
                        critChance += adrenaline.CritChance;
            }
            // --------------

            // Critter
            var critter = owner.gameObject.GetComponent<PlayerCritter>();
            if (critter != null)
            {
                critChance += critter.CritChance;
                if (critChance >= rnd.Next(0, 101))
                {
                    critter.DisplayCrit(other.transform.position);
                    atkMultiplier += 1;
                }
            }
            // --------------

            var otherHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (owner.CompareTag("Player"))
            {
                owner.Hit();
                if (otherHealth.Health.Value - damage * atkMultiplier <= 0)
                {
                    owner.Kill();
                }
            }
            owner.RequestAttack(otherHealth.Health, damage * atkMultiplier);
            var m = (new Vector2(other.transform.position.x - transform.position.x,
                -(other.transform.position.y - transform.position.y))).normalized * knockback * 3;
            other.attachedRigidbody.velocity += m;
            
            
        }
        else if(other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            // Bubble Barrier
            var bubbleBarrier = other.gameObject.GetComponentInChildren<PlayerBubbleBarrier>();
            if (bubbleBarrier != null)
            {
                bubbleBarrier.PopBubble();
                return;
            }
            // --------------
            
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
