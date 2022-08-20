using System;
using Statistics;
using UnityEngine;

namespace HUD
{
    public class HealthHUDTest : MonoBehaviour
    {
        private Health health;
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
        }
    }
}