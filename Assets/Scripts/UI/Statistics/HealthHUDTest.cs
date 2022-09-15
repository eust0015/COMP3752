﻿using System;
using UnityEngine;

namespace UI.Statistics
{
    [Serializable]
    public class HealthHUDTest : MonoBehaviour
    {
        [SerializeField] private Health health;

        public Health Health
        {
            get => health;
            private set => health = value;
        }

        private void OnEnable()
        {
            health = transform.GetComponent<HealthHUD>().Health;
            health.IncreaseMaxValue(20);
            health.IncreaseValue(20);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                health.DecreaseValue(1);
            else if (Input.GetMouseButtonDown(1))
                health.IncreaseValue(1);
        }
    }
}