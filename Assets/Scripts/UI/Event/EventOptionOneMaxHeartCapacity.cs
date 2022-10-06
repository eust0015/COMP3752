using System;
using Player;
using UI.RelicInventory;
using UI.Relics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Event
{
    [Serializable]
    public class EventOptionOneMaxHeartCapacity : EventOption
    {
        [SerializeField] private MaxHeartCapacityRelic relic;

        public MaxHeartCapacityRelic Relic
        {
            get => relic;
            private set => relic = value;
        }

        public override void Choose()
        {
            RelicInventoryUI relicInventory = Object.FindObjectOfType<RelicInventoryUI>();
            relicInventory.AddRelic(Relic);
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            health.Health.IncreaseMaxValue(2);
        }
    }
}