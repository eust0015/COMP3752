using System;
using HUD;
using Items;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class ShopItemUI :MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image icon;
        
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public void Initialise(Item setItem)
        {
            Item = setItem;
            itemName.text = Item.Name;
            description.text = Item.Description;
            price.text = "$" + Item.Price;
            icon.sprite = Item.Icon;
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
        
    }
}