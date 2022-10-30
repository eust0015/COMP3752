using System;
using System.Collections;
using System.Collections.Generic;
using Player.Relics;
using UI.Statistics;
using UnityEngine;
using Random = System.Random;

public class PollenMovement : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int pollenMultiplier = 1;

            // PlayerStats
            var playerStats = other.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                pollenMultiplier = (int)Math.Round(playerStats.pollenMultiplier);
            }
            // --------------
            
            // Thorough Forage
            var thoroughForage = other.gameObject.GetComponent<PlayerThoroughForage>();
            if (thoroughForage != null)
            {
                Random rnd = new Random();
                if (thoroughForage.CritChance >= rnd.Next(1, 101))
                {
                    thoroughForage.DisplayThoroughForage(transform.position);
                    pollenMultiplier += 1;
                }
                    
            }
            // --------------
            
            var pollenHUD = FindObjectOfType<CurrencyHUD>();
            pollenHUD.Currency.IncreaseValue(1 * pollenMultiplier);
            Destroy(gameObject);
        }
    }
}
