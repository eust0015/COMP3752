using Player.Relics;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreaseAdrenalineMultiplier : EffectUI
    {
        [SerializeField] private int critChance;

        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public override void DoEffect()
        {
            PlayerAdrenaline adrenaline = FindObjectOfType<PlayerAdrenaline>();
            adrenaline.CritChance += CritChance;
        }
    }
}