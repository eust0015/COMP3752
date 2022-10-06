using System;
using UI.Relics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.RelicInventory
{
    [Serializable]
    public class InventoryRelicUI : MonoBehaviour 
    {
        [SerializeField] private Relic relic;
        [SerializeField] private Image icon;
        [SerializeField] private Transform mouseOverContainer;
        [SerializeField] private InventoryRelicDetailedUI mouseOverPrefab;
        [SerializeField] private InventoryRelicDetailedUI activeMouseOver;

        public delegate void Destroyed(InventoryRelicUI thisObject);
        public event Destroyed OnDestroyed;
        public Relic Relic
        {
            get => relic;
            private set => relic = value;
        }

        public Image Icon
        {
            get => icon;
            private set => icon = value;
        }

        public Transform MouseOverContainer
        {
            get => mouseOverContainer;
            private set => mouseOverContainer = value;
        }

        public InventoryRelicDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public InventoryRelicDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }

        public void Initialise(Relic setRelic)
        {
            Relic = setRelic;
            UpdateSprite();
            HideDetailedDescription();
        }

        public void UpdateSprite() => icon.sprite = Relic.Icon;
        
        public void Use()
        {
            Relic.Use();
        }

        public void ShowDetailedDescription()
        {
            activeMouseOver = Instantiate(mouseOverPrefab, MouseOverContainer);
            activeMouseOver.Initialise(Relic);
        }

        public void HideDetailedDescription()
        {
            if (activeMouseOver == null)
                return;
            
            Destroy(activeMouseOver.gameObject);
            activeMouseOver = null;    
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}