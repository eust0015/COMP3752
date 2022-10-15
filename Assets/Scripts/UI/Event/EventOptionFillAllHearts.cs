using System;
using Player;
using Object = UnityEngine.Object;

namespace UI.Event
{
    [Serializable]
    public class EventOptionFillAllHearts : EventOption
    {
        public override void Choose()
        {
            PlayerHealth health = Object.FindObjectOfType<PlayerHealth>();
            health.Health.IncreaseValue(999);
        }
    }
}