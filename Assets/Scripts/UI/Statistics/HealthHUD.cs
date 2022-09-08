using System;
using System.Collections.Generic;
using Player;
using Statistics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [Serializable]
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Transform container;
        [SerializeField] private HeartHUD heartPrefab;
        [SerializeField] private List<HeartHUD> heartList;

        public Health Health
        {
            get => health;
            private set
            {
                UnsubscribeFromEvents();
                health = value;
                SubscribeToEvents();
            }
        }

        public Transform Container { get; private set; }
        public HeartHUD HeartPrefab { get; private set; }
        private List<HeartHUD> HeartList { get; set; }

        private void OnEnable()
        {
            PlayerHealth playerHealth = GetComponent<PlayerHealth>();

            if (playerHealth != null)
                Health = playerHealth.Health;
            else if (Health != null)
                SubscribeToEvents();
            else
                Health = new Health();
            
            MaxHealthIncreased();
        }
        
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            if (Health == null)
                return;
            
            Health.OnMaxValueIncreased += MaxHealthIncreased;
        }

        private void UnsubscribeFromEvents()
        {
            if (Health == null)
                return;
            
            Health.OnMaxValueIncreased -= MaxHealthIncreased;
        }

        public void MaxHealthIncreased()
        {
            if (HeartList == null)
                HeartList = new List<HeartHUD>();

            if (Health.MaxValue < HeartList.Count * 2)
                return;

            for (int i = HeartList.Count + 1; i < Health.MaxValue + 1; i+=2)
            {
                
            }
        }

        public void AddHeart(int halfHeart)
        {

            HeartHUD heart = Instantiate(HeartPrefab, Container);
            heart.Initialise(Health, halfHeart);
            heartList.Add(heart);
            heart.OnDestroyed += RemoveItemFromList;
        }
        
        public void RemoveItemFromList(HeartHUD itemUI)
        {
            itemUI.OnDestroyed -= RemoveItemFromList;
            heartList.Remove(itemUI);
        }
        
    }
}