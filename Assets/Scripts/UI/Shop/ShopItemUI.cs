using System;
using System.Collections.Generic;
using Audio;
using TMPro;
using UI.AbilityInventory;
using UI.Effect;
using UI.ItemInventory;
using UI.Items;
using UI.RelicInventory;
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
        [SerializeField] private List<InventoryAbilityUI> abilityList;
        [SerializeField] private List<InventoryRelicUI> relicList;
        [SerializeField] private List<InventoryItemUI> itemList;
        [SerializeField] private List<EffectUI> effectList;
        [SerializeField] private FMODAudioSource buySound;
        [SerializeField] private FMODAudioSource insufficientFundsSound;
        
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
        
        public List<InventoryAbilityUI> AbilityList
        {
            get => abilityList;
            private set => abilityList = value;
        }

        public List<InventoryRelicUI> RelicList
        {
            get => relicList;
            private set => relicList = value;
        }

        public List<InventoryItemUI> ItemList
        {
            get => itemList;
            private set => itemList = value;
        }

        public List<EffectUI> EffectList
        {
            get => effectList;
            set => effectList = value;
        }

        public FMODAudioSource BuySound
        {
            get => buySound;
            private set => buySound = value;
        }

        public FMODAudioSource InsufficientFundsSound
        {
            get => insufficientFundsSound;
            private set => insufficientFundsSound = value;
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
                InsufficientFundsSound.PlaySound();
                return;
            }
            BuySound.PlaySound();
            currency.DecreaseValue(Item.Price);
            
            foreach (var ability in AbilityList)
            {
                AbilityInventoryUI abilityInventory = FindObjectOfType<AbilityInventoryUI>();
                abilityInventory.AddAbility(ability);
            }
            foreach (var relic in RelicList)
            {
                RelicInventoryUI relicInventory = FindObjectOfType<RelicInventoryUI>();
                relicInventory.AddInventoryRelicUI(relic);
            }
            foreach (var itemBought in ItemList)
            {
                ItemInventoryUI itemInventory = FindObjectOfType<ItemInventoryUI>();
                itemInventory.AddItem(itemBought);
            }
            foreach (var effect in EffectList)
            {
                Instantiate(effect);
            }
        }
        
        public void UpdatePrice() => price.text = Item.Price.ToString();
        public void UpdateSprite() => icon.sprite = Item.Icon;

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
        
        public virtual void DisplayInsufficientFundsMessage() => Instantiate(messagePrefab, transform);

    }
}