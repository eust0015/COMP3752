using System;
using TMPro;
using UI.Relics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RelicInventory
{
    [Serializable]
    public class InventoryRelicDetailedUI :MonoBehaviour 
    {
        [SerializeField] private Relic relic;
        [SerializeField] private TMP_Text relicName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;

        public delegate void Destroyed(InventoryRelicDetailedUI thisObject);
        public event Destroyed OnDestroyed;
        public Relic Relic
        {
            get => relic;
            private set => relic = value;
        }

        public TMP_Text RelicName
        {
            get => relicName;
            private set => relicName = value;
        }

        public TMP_Text Description
        {
            get => description;
            private set => description = value;
        }

        public Image Icon
        {
            get => icon;
            private set => icon = value;
        }

        public void Initialise(Relic setRelic)
        {
            Relic = setRelic;
            UpdateName();
            UpdateDescription();
            UpdateSprite();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (Relic == null)
                return;
        }

        private void UnsubscribeFromEvents()
        {
            if (Relic == null)
                return;
        }
        
        public void UpdateName() => relicName.text = Relic.Name;
        public void UpdateDescription() => description.text = Relic.Description;
        public void UpdateSprite() => icon.sprite = Relic.Icon;

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            OnDestroyed?.Invoke(this);
        }
    }
}