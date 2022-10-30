using UI.Statistics;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreasePollen : EffectUI
    {
        [SerializeField] private int pollenAmount;

        public int PollenAmount
        {
            get => pollenAmount;
            set => pollenAmount = value;
        }

        public override void DoEffect()
        {
            CurrencyHUD pollenHUD = FindObjectOfType<CurrencyHUD>();
            pollenHUD.Currency.IncreaseValue(PollenAmount);
        }
    }
}