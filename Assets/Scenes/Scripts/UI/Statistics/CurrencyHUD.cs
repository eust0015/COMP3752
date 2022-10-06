using System;
using TMPro;
using UnityEngine;

namespace UI.Statistics
{
    [Serializable]
    public class CurrencyHUD : MonoBehaviour
    {
        [SerializeField] private Currency currency;
        [SerializeField] public TMP_Text text;
        
        public Currency Currency
        {
            get => currency;
            private set
            {
                UnsubscribeFromEvents();
                currency = value;
                SubscribeToEvents();
            }
        }

        public TMP_Text Text
        {
            get => text;
            private set => text = value;
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
            if (Currency == null)
                return;
            
            Currency.OnValueIncreased += UpdateDisplay;
            Currency.OnMaxValueIncreased += UpdateDisplay;
            Currency.OnValueDecreased += UpdateDisplay;
            Currency.OnMaxValueDecreased += UpdateDisplay;
        }

        private void UnsubscribeFromEvents()
        {
            if (Currency == null)
                return;
            
            Currency.OnValueIncreased -= UpdateDisplay;
            Currency.OnMaxValueIncreased -= UpdateDisplay;
            Currency.OnValueDecreased -= UpdateDisplay;
            Currency.OnMaxValueDecreased -= UpdateDisplay;
        }
        
        public void UpdateDisplay()
        {
            text.text = Currency.Value.ToString();
        }

    }
}