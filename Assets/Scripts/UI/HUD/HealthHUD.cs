using System;
using Statistics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HUD
{
    [Serializable]
    public class HealthHUD : MonoBehaviour
    {
        [SerializeField] private Health health;
        [SerializeField] private Slider slider;

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

        private void OnEnable()
        {
            UpdateDisplay();
            SubscribeToEvents();
        }
        
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            if (Health == null)
                return;
            
            Health.OnValueIncreased += UpdateDisplay;
            Health.OnValueDecreased += UpdateDisplay;
            Health.OnMaxValueIncreased += UpdateDisplay;
            Health.OnMaxValueDecreased += UpdateDisplay;
        }

        private void UnsubscribeFromEvents()
        {
            if (Health == null)
                return;
            
            Health.OnValueIncreased -= UpdateDisplay;
            Health.OnValueDecreased -= UpdateDisplay;
            Health.OnMaxValueIncreased -= UpdateDisplay;
            Health.OnMaxValueDecreased -= UpdateDisplay;
        }
        
        public void UpdateDisplay()
        {
            slider.value = (float) Health.Value / Health.MaxValue;
        }
    }
}