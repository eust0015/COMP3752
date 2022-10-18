using System;
using System.Collections.Generic;
using Player;
using UnityEngine;

namespace UI.Statistics
{
    [Serializable]
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Transform container;
        [SerializeField] private HeartHUD heartPrefab;
        [SerializeField] private List<HeartHUD> heartList;

        [SerializeField] public Health Health
        {
            get => health;
            private set
            {
                UnsubscribeFromEvents();
                health = value;
                SubscribeToEvents();
            }
        }

        public Transform Container
        {
            get => container;
            private set => container = value;
        }

        public HeartHUD HeartPrefab
        {
            get => heartPrefab;
            private set => heartPrefab = value;
        }

        public List<HeartHUD> HeartList
        {
            get => heartList;
            private set => heartList = value;
        }

        private void OnEnable()
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        
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

            if (Health.MaxValue <= HeartList.Count * 2)
                return;

            for (int i = HeartList.Count * 2 + 1; i < Health.MaxValue + 1; i+=2)
            {
                AddHeart(i);
            }
        }

        public void AddHeart(int halfHeart)
        {

            HeartHUD heart = Instantiate(heartPrefab, container);
            heart.Initialise(Health, halfHeart);
            HeartList.Add(heart);
            heart.OnDestroyed += RemoveItemFromList;
        }
        
        public void RemoveItemFromList(HeartHUD itemUI)
        {
            itemUI.OnDestroyed -= RemoveItemFromList;
            HeartList.Remove(itemUI);
        }
        
    }
}