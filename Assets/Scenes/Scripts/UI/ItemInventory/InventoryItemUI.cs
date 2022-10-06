using System;
using TMPro;
using UI.Inventory;
using UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ItemInventory
{
    [Serializable]
    public class InventoryItemUI : MonoBehaviour 
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text quantity;
        [SerializeField] private Image icon;
        [SerializeField] private Transform mouseOverContainer;
        [SerializeField] private InventoryItemDetailedUI mouseOverPrefab;
        [SerializeField] private InventoryItemDetailedUI activeMouseOver;

        public delegate void Destroyed(InventoryItemUI thisObject);
        public event Destroyed OnDestroyed;
        public Item Item
        {
            get => item;
            private set => item = value;
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

        public Transform MouseOverContainer
        {
            get => mouseOverContainer;
            private set => mouseOverContainer = value;
        }

        public InventoryItemDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public InventoryItemDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }

        public void Initialise(Item setItem)
        {
            Item = setItem;
            SubscribeToEvents();
            UpdateQuantity();
            UpdateSprite();
            HideDetailedDescription();
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
        
        public void UpdateQuantity() => quantity.text = Item.Quantity.ToString();
        public void UpdateSprite() => icon.sprite = Item.Icon;
        
        public void Use()
        {
            if (Item.Quantity < 1)
                return;

            Item.Use();
            Item.Quantity = Item.Quantity - 1;
            if (Item.Quantity < 1)
                Destroy(gameObject);
        }

        public void ShowDetailedDescription()
        {
            activeMouseOver = Instantiate(mouseOverPrefab, MouseOverContainer);
            activeMouseOver.Initialise(Item);
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
            UnsubscribeFromEvents();
        }
    }
}