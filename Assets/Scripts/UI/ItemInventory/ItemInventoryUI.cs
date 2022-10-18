using System;
using System.Collections.Generic;
using System.Linq;
using UI.Inventory;
using UI.Items;
using UnityEngine;

namespace UI.ItemInventory
{
    [Serializable]
    public class ItemInventoryUI : MonoBehaviour
    {

        [SerializeField] private Transform itemContainer;
        [SerializeField] private InventoryItemUI inventoryItemPrefab;
        [SerializeField] private List<InventoryItemUI> inventoryItemsList;

        public Transform ItemContainer
        {
            get => itemContainer;
            private set => itemContainer = value;
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

        public void Display(List<InventoryItemUI> items)
        {
            Clear();

            foreach (InventoryItemUI item in items)
            {
                AddItem(item);
            }
        }

        public void AddItem(InventoryItemUI item)
        {
            InventoryItemUI inventoryItem = InventoryItemsList.FirstOrDefault(p => p.Item.Type == item.Item.Type);
            
            // If the item was found in the list
            if (inventoryItem != null)
            {
                inventoryItem.Item.Quantity += 1;
            }
            else // If the item was not found in the list
            {
                inventoryItem = Instantiate(item, ItemContainer);
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