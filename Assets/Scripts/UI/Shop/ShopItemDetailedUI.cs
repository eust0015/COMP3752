using System;
using Items;
using TMPro;
using UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    [Serializable]
    public class ShopItemDetailedUI :MonoBehaviour 
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image icon;

        public delegate void Destroyed(ShopItemDetailedUI thisObject);
        public event Destroyed OnDestroyed;
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public void Initialise(Item setItem)
        {
            Item = setItem;
            UpdateName();
            UpdateDescription();
            UpdatePrice();
            UpdateSprite();
        }

        public void UpdateName() => itemName.text = Item.Name;
        public void UpdateDescription() => description.text = Item.Description;
        public void UpdatePrice() => price.text = Item.Price.ToString();
        public void UpdateSprite() => icon.sprite = Item.Icon;
        private void OnDestroy() => OnDestroyed?.Invoke(this);

    }
}