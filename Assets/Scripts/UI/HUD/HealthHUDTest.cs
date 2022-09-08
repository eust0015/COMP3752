using System;
using Statistics;
using UI.HUD;
using UnityEngine;

namespace HUD
{
    [Serializable]
    public class HealthHUDTest : MonoBehaviour
    {
        [SerializeField] private Health health;
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