using Player.Relics;
using UnityEngine;

namespace UI.Effect
{
    public class EffectIncreaseHeartStealerChance : EffectUI
    {
        [SerializeField] private int heartStealChance;

        public int HeartStealChance
        {
            get => heartStealChance;
            set => heartStealChance = value;
        }

        public override void DoEffect()
        {
            PlayerHeartStealer heartStealer = FindObjectOfType<PlayerHeartStealer>();
            heartStealer.HeartStealChance += HeartStealChance;
        }
    }
}