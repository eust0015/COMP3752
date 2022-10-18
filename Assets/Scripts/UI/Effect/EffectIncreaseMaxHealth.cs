using Player;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreaseMaxHealth : EffectUI
    {
        [SerializeField] private int healthAmount;

        public int HealthAmount
        {
            get => healthAmount;
            set => healthAmount = value;
        }

        public override void DoEffect()
        {
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            health.Health.IncreaseMaxValue(HealthAmount);
        }
    }
}