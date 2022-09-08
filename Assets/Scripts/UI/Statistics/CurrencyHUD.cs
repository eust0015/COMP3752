﻿using System;
using Statistics;
using TMPro;
using UnityEngine;

namespace HUD
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
        }

        private void UnsubscribeFromEvents()
        {
            if (Currency == null)
                return;
            
            Currency.OnValueIncreased -= UpdateDisplay;
            Currency.OnMaxValueIncreased -= UpdateDisplay;
        }
        
        public void UpdateDisplay()
        {
            text.text = Currency.Value.ToString();
        }

    }
}