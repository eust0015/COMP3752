using System;
using System.Collections.Generic;
using System.Linq;
using UI.Relics;
using UnityEngine;

namespace UI.RelicInventory
{
    [Serializable]
    public class RelicInventoryUI : MonoBehaviour
    {

        [SerializeField] private Transform relicContainer;
        [SerializeField] private InventoryRelicUI inventoryRelicPrefab;
        [SerializeField] private List<InventoryRelicUI> inventoryRelicsList;

        public Transform RelicContainer
        {
            get => relicContainer;
            private set => relicContainer = value;
        }

        public InventoryRelicUI InventoryRelicPrefab
        {
            get => inventoryRelicPrefab;
            private set => inventoryRelicPrefab = value;
        }

        public List<InventoryRelicUI> InventoryRelicsList
        {
            get
            {
                if (inventoryRelicsList == null)
                    inventoryRelicsList = new List<InventoryRelicUI>();
                return inventoryRelicsList;
            }
            private set => inventoryRelicsList = value;
        }

        public void Display(List<Relic> relics)
        {
            Clear();

            foreach (Relic relic in relics)
            {
                AddRelic(relic);
            }
        }
        
        public void AddRelic(Relic relic)
        {
            InventoryRelicUI inventoryRelic = InventoryRelicsList.FirstOrDefault(p => p.Relic.GetType() == relic.GetType());
            
            // If the relic was found in the list
            if (inventoryRelic != null)
            {
                inventoryRelic.Relic.Quantity += 1;
            }
            else // If the relic was not found in the list
            {
                inventoryRelic = Instantiate(InventoryRelicPrefab, RelicContainer);
                inventoryRelic.Initialise(relic);
                InventoryRelicsList.Add(inventoryRelic);
                inventoryRelic.OnDestroyed += RemoveRelicFromList;
            }
        }

        public void Clear()
        {
            foreach (InventoryRelicUI menuRelic in InventoryRelicsList)
            {
                Destroy(menuRelic.gameObject);
            }
            InventoryRelicsList.Clear();
        }

        public void RemoveRelicFromList(InventoryRelicUI relicUI)
        {
            relicUI.OnDestroyed -= RemoveRelicFromList;
            InventoryRelicsList.Remove(relicUI);
        }
        
    }
}