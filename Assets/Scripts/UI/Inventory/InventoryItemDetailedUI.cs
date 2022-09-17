using System;
using TMPro;
using UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    [Serializable]
    public class InventoryItemDetailedUI :MonoBehaviour 
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text quantity;
        [SerializeField] private Image icon;

        public delegate void Destroyed(InventoryItemDetailedUI thisObject);
        public event Destroyed OnDestroyed;
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public TMP_Text ItemName
        {
            get => itemName;
            private set => itemName = value;
        }

        public TMP_Text Description
        {
            get => description;
            private set => description = value;
        }

        public TMP_Text Quantity
        {
            get => quantity;
            private set => quantity = value;
        }

        public Image Icon
        {
            get => icon;
            private set => icon = value;
        }

        public void Initialise(Item setItem)
        {
            Item = setItem;
            UpdateName();
            UpdateDescription();
            UpdateQuantity();
            UpdateSprite();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (Item == null)
                return;
            
            Item.OnQuantityChanged += UpdateQuantity;
        }

        private void UnsubscribeFromEvents()
        {
            if (Item == null)
                return;
            
            Item.OnQuantityChanged -= UpdateQuantity;
        }
        
        public void UpdateName() => itemName.text = Item.Name;
        public void UpdateDescription() => description.text = Item.Description;
        public void UpdateQuantity() => quantity.text = Item.Quantity.ToString();
        public void UpdateSprite() => icon.sprite = Item.Icon;

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            OnDestroyed?.Invoke(this);
        }
    }
}