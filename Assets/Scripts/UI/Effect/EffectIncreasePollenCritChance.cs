using Player.Relics;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreasePollenCritChance : EffectUI
    {
        [SerializeField] private int critChance;

        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public override void DoEffect()
        {
            PlayerThoroughForage thoroughForage = FindObjectOfType<PlayerThoroughForage>();
            thoroughForage.CritChance += CritChance;
        }
    }
}