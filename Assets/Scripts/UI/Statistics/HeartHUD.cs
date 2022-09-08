using System;
using Player;
using Statistics;
using UnityEngine;

namespace UI.HUD
{
    [Serializable]
    public class HeartHUD : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private int halfHeart;
        [SerializeField] private Animator heartAnimator;
        private static readonly int Heart = Animator.StringToHash("heart");

        public delegate void Destroyed(HeartHUD thisObject);
        public event Destroyed OnDestroyed;
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
        
        [SerializeField] public int HalfHeart { get; private set; }
        [SerializeField] public Animator HeartAnimator { get; private set; }
        
        public void Initialise(Health setHealth, int setHalfHeart)
        {

            HalfHeart = setHalfHeart;
            
            if (setHealth != null)
                Health = setHealth;
            else if (Health != null)
                SubscribeToEvents();
            else
                Health = new Health();
            
            UpdateDisplay();
        }

        private void SubscribeToEvents()
        {
            if (Health == null)
                return;
            
            Health.OnValueIncreased += UpdateDisplay;
            Health.OnValueDecreased += UpdateDisplay;
            Health.OnMaxValueDecreased += MaxHealthDecreased;
        }

        private void UnsubscribeFromEvents()
        {
            if (Health == null)
                return;
            
            Health.OnValueIncreased -= UpdateDisplay;
            Health.OnValueDecreased -= UpdateDisplay;
            Health.OnMaxValueDecreased -= MaxHealthDecreased;
        }
        
        public void UpdateDisplay()
        {
            if (Health.Value > HalfHeart)
                heartAnimator.SetInteger(Heart, 2);
            else if (Health.Value < HalfHeart)
                heartAnimator.SetInteger(Heart, 0);
            else
                heartAnimator.SetInteger(Heart, 1);
        }

        public void MaxHealthDecreased()
        {
            if (Health.MaxValue < HalfHeart - 1)
                Destroy(this);
        }
        
        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
            UnsubscribeFromEvents();
        }
    }
}