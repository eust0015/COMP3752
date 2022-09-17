using System;
using System.Collections.Generic;
using System.Linq;
using UI.Items;
using UnityEngine;

namespace UI.Inventory
{
    [Serializable]
    public class InventoryUI : MonoBehaviour
    {

        [SerializeField] private Transform container;
        [SerializeField] private InventoryItemUI inventoryItemPrefab;
        [SerializeField] private List<InventoryItemUI> inventoryItemsList;

        public Transform Container
        {
            get => container;
            private set => container = value;
        }

        public InventoryItemUI InventoryItemPrefab
        {
            get => inventoryItemPrefab;
            private set => inventoryItemPrefab = value;
        }

        public List<InventoryItemUI> InventoryItemsList
        {
            get
            {
                if (inventoryItemsList == null)
                    inventoryItemsList = new List<InventoryItemUI>();
                return inventoryItemsList;
            }
            private set => inventoryItemsList = value;
        }

        public void Display(List<Item> items)
        {
            Clear();

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }
        
        public void AddItem(Item item)
        {
            InventoryItemUI inventoryItem = InventoryItemsList.FirstOrDefault(p => p.Item.GetType() == item.GetType());
            
            // If the item was found in the list
            if (inventoryItem != null)
            {
                inventoryItem.Item.Quantity += 1;
            }
            else // If the item was not found in the list
            {
                inventoryItem = Instantiate(InventoryItemPrefab, Container);
                inventoryItem.Initialise(item);
                InventoryItemsList.Add(inventoryItem);
                inventoryItem.OnDestroyed += RemoveItemFromList;
            }
        }

        public void Clear()
        {
            foreach (InventoryItemUI menuItem in InventoryItemsList)
            {
                Destroy(menuItem.gameObject);
            }
            InventoryItemsList.Clear();
        }

        public void RemoveItemFromList(InventoryItemUI itemUI)
        {
            itemUI.OnDestroyed -= RemoveItemFromList;
            InventoryItemsList.Remove(itemUI);
        }
        
    }
}