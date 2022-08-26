using System;
using Statistics;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
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
            
            Health.OnValueChanged += UpdateDisplay;
            Health.OnMaxValueChanged += UpdateDisplay;
        }

        private void UnsubscribeFromEvents()
        {
            if (Health == null)
                return;
            
            Health.OnValueChanged -= UpdateDisplay;
            Health.OnMaxValueChanged -= UpdateDisplay;
        }
        
        public void UpdateDisplay()
        {
            slider.value = (float) Health.Value / Health.MaxValue;
        }
    }
}