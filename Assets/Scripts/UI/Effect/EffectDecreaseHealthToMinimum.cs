using Player;
using UnityEngine;

namespace UI.Effect
{
    public class EffectDecreaseHealthToMinimum : EffectUI
    {
        
        [SerializeField] private int minimumHealthAmount;

        public int MinimumHealthAmount
        {
            get => minimumHealthAmount;
            set => minimumHealthAmount = value;
        }
        
        public override void DoEffect()
        {
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            if (health.Health.Value > MinimumHealthAmount)
                health.Health.DecreaseValue(health.Health.Value - MinimumHealthAmount);
        }
    }
}