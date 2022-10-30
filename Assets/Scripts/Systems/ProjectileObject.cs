using System;
using Enemy;
using Player;
using Player.Relics;
using UI.Statistics;
using UnityEngine;
using Random = System.Random;

namespace Systems
{
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
                    if (heartStealChance >= rnd.Next(1, 101))
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
                    if (critChance >= rnd.Next(1, 101))
                    {
                        critter.DisplayCrit(other.transform.position);
                        atkMultiplier += 1;
                    }
                }
                // --------------
            
                Debug.Log("AttackEnemy");
                var otherHealth = other.gameObject.GetComponent<EnemyHealth>();
                Attack(otherHealth.Health, damage * atkMultiplier);
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
}
