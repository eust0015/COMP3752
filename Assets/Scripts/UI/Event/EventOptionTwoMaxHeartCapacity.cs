using System;
using Player;
using UI.RelicInventory;
using UI.Relics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.Event
{
    [Serializable]
    public class EventOptionTwoMaxHeartCapacity : EventOption
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
            relicInventory.AddRelic(Relic);
            
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            health.Health.DecreaseValue(health.Health.Value - 2);
            health.Health.IncreaseMaxValue(4);
        }
    }
}