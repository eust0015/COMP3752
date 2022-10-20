using System;
using System.Collections.Generic;
using System.Linq;
using UI.Abilities;
using UnityEngine;

namespace UI.AbilityInventory
{
    [System.Serializable]
    public class AbilityInventoryUI : MonoBehaviour
    {

        [SerializeField] private Transform abilityContainer;
        [SerializeField] private InventoryAbilityUI inventoryAbilityPrefab;
        [SerializeField] private List<InventoryAbilityUI> inventoryAbilitiesList;

        public Transform AbilityContainer
        {
            get => abilityContainer;
            private set => abilityContainer = value;
        }

        public InventoryAbilityUI InventoryAbilityPrefab
        {
            get => inventoryAbilityPrefab;
            private set => inventoryAbilityPrefab = value;
        }

        public List<InventoryAbilityUI> InventoryAbilitiesList
        {
            get
            {
                if (inventoryAbilitiesList == null)
                    inventoryAbilitiesList = new List<InventoryAbilityUI>();
                return inventoryAbilitiesList;
            }
            private set => inventoryAbilitiesList = value;
        }

        public void OnEnable()
        {
            foreach (InventoryAbilityUI inventoryAbility in InventoryAbilitiesList)
            {
                inventoryAbility.OnDestroyed += RemoveAbilityFromList;
            }
        }

        public void Display(List<InventoryAbilityUI> abilities)
        {
            Clear();

            foreach (InventoryAbilityUI ability in abilities)
            {
                AddAbility(ability);
            }
        }

        public void AddAbility(InventoryAbilityUI ability)
        {
            InventoryAbilityUI inventoryAbility = Instantiate(ability, AbilityContainer);
            InventoryAbilitiesList.Add(inventoryAbility);
            inventoryAbility.OnDestroyed += RemoveAbilityFromList;
        }

        public void Clear()
        {
            foreach (InventoryAbilityUI menuAbility in InventoryAbilitiesList)
            {
                Destroy(menuAbility.gameObject);
            }
            InventoryAbilitiesList.Clear();
        }

        public void RemoveAbilityFromList(InventoryAbilityUI abilityUI)
        {
            abilityUI.OnDestroyed -= RemoveAbilityFromList;
            InventoryAbilitiesList.Remove(abilityUI);
        }
        
    }
}