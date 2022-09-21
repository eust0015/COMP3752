using System;
using TMPro;
using UI.Inventory;
using UI.Items;
using UI.Statistics;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    [Serializable]
    public class ShopItemUI :MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image icon;
        [SerializeField] private Transform mouseOverContainer;
        [SerializeField] private ShopItemDetailedUI mouseOverPrefab;
        [SerializeField] private ShopItemDetailedUI activeMouseOver;
        [SerializeField] private GameObject messagePrefab;
        
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public TMP_Text Price
        {
            get => price;
            private set => price = value;
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
        
        public ShopItemDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public ShopItemDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }
        
        public GameObject MessagePrefab
        {
            get => messagePrefab;
            private set => messagePrefab = value;
        }
        
        public void Initialise(Item setItem, Transform setMouseOverContainer)
        {
            Item = setItem;
            UpdatePrice();
            UpdateSprite();
            MouseOverContainer = setMouseOverContainer;
        }
        
        public void Buy()
        {
            Currency currency = FindObjectOfType<CurrencyHUD>().Currency;

            if (currency == null)
                return;

            if (Item.Price > currency.Value)
            {
                DisplayInsufficientFundsMessage();
                return;
            }
            
            currency.DecreaseValue(Item.Price);
            
            InventoryUI inventory = FindObjectOfType<InventoryUI>();
            inventory.AddItem(Item);
        }
        
        public void UpdatePrice() => price.text = Item.Price.ToString();
        public void UpdateSprite() => icon.sprite = Item.Icon;

        public void ShowDetailedDescription()
        {
            activeMouseOver = Instantiate(mouseOverPrefab, MouseOverContainer);
            activeMouseOver.Initialise(item);
        }

        public void HideDetailedDescription()
        {
            if (activeMouseOver == null)
                return;
            
            Destroy(activeMouseOver.gameObject);
            activeMouseOver = null;    
        }
        
        public virtual void DisplayInsufficientFundsMessage() => Instantiate(messagePrefab, transform);

    }
}