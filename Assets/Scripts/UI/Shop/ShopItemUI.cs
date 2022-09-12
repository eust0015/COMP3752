using System;
using HUD;
using Statistics;
using TMPro;
using UI.Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Shop
{
    [Serializable]
    public class ShopItemUI :MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private MouseOverShopItemUI mouseOverUI;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image icon;
        
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public MouseOverShopItemUI MouseOverUI
        {
            get => mouseOverUI;
            private set => mouseOverUI = value;
        }
        
        public void Initialise(Item setItem)
        {
            Item = setItem;
            price.text = Item.Price.ToString();
            icon.sprite = Item.Icon;
            MouseOverUI = FindObjectOfType<MouseOverShopItemUI>();
        }
        
        public void Buy()
        {
            Currency currency = FindObjectOfType<CurrencyHUD>().Currency;

            if (currency == null)
                return;

            if (Item.Price > currency.Value)
            {
                InsufficientFundsMessageUI message = FindObjectOfType<InsufficientFundsMessageUI>();
                message.Display();
                return;
            }
            
            currency.DecreaseValue(Item.Price);
            
            InventoryUI inventory = FindObjectOfType<InventoryUI>();
            inventory.AddItem(Item);
        }
        
        public void ShowDetailedDescription() => MouseOverUI.Display(Item);
        public void HideDetailedDescription() => MouseOverUI.Hide();
    }
}