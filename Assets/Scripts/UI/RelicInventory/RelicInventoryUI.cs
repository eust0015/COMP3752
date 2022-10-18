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
            InventoryRelicUI inventoryRelic = Instantiate(InventoryRelicPrefab, RelicContainer);
            inventoryRelic.Initialise(relic);
            InventoryRelicsList.Add(inventoryRelic);
            inventoryRelic.OnDestroyed += RemoveRelicFromList;
        }

        public void AddInventoryRelicUI(InventoryRelicUI relic)
        {
            InventoryRelicUI inventoryRelic = Instantiate(relic, RelicContainer);
            InventoryRelicsList.Add(inventoryRelic);
            inventoryRelic.OnDestroyed += RemoveRelicFromList;
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