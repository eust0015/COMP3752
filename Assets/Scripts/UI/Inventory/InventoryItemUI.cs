using System;
using TMPro;
using UI.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    [Serializable]
    public class InventoryItemUI :MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler 
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text quantity;
        [SerializeField] private Image icon;

        public delegate void Destroyed(InventoryItemUI thisObject);
        public event Destroyed OnDestroyed;
        public Item Item
        {
            get => item;
            private set => item = value;
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
            MouseOverInventoryItemUI mouseOver = FindObjectOfType<MouseOverInventoryItemUI>();
            mouseOver.Display(Item);
        }

        public void HideDetailedDescription()
        {
            MouseOverInventoryItemUI mouseOver = FindObjectOfType<MouseOverInventoryItemUI>();
            mouseOver.Hide();
        }

        // public void OnPointerEnter(PointerEventData eventData)
        // {
        //     ShowDetailedDescription();
        // }
        //
        // public void OnPointerExit(PointerEventData eventData)
        // {
        //     HideDetailedDescription();
        // }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
            UnsubscribeFromEvents();
        }
    }
}