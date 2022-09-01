using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class ShopUI : MonoBehaviour
    {

        [SerializeField] private Transform container;
        [SerializeField] private ShopItemUI shopItemPrefab;
        [SerializeField] private List<ShopItemUI> shopItemsList;

        public Transform Container
        {
            get => container;
            private set => container = value;
        }

        public ShopItemUI ShopItemPrefab
        {
            get => shopItemPrefab;
        }

        public List<ShopItemUI> ShopItemsList
        {
            get
            {
                if (shopItemsList == null)
                    shopItemsList = new List<ShopItemUI>();
                return shopItemsList;
            }
            private set => shopItemsList = value;
        }

        public void Display(List<Item> items)
        {
            Clear();

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }
        
        private void AddItem(Item item)
        {
            ShopItemUI shopItem = Instantiate(ShopItemPrefab, Container);
            shopItem.Initialise(item);
            ShopItemsList.Add(shopItem);
        }

        public void Clear()
        {
            foreach (ShopItemUI menuItem in ShopItemsList)
            {
                Destroy(menuItem.gameObject);
            }
            ShopItemsList.Clear();
        }
        
    }
}