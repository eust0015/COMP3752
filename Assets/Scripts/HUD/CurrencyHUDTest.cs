using System;
using Statistics;
using UnityEngine;

namespace HUD
{
    public class CurrencyHUDTest : MonoBehaviour
    {
        private Currency currency;
        private void OnEnable()
        {
            currency = transform.GetComponent<CurrencyHUD>().Currency;
            currency.IncreaseMaxValue(30);
            currency.IncreaseValue(20);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                currency.DecreaseValue(1);
            else if (Input.GetMouseButtonDown(1))
                currency.IncreaseValue(1);
        }
    }
}