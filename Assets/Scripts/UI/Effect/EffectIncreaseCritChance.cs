using Player.Relics;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreaseCritChance : EffectUI
    {
        [SerializeField] private int critChance;

        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public override void DoEffect()
        {
            PlayerCritter critter = FindObjectOfType<PlayerCritter>();
            critter.CritChance += CritChance;
        }
    }
}