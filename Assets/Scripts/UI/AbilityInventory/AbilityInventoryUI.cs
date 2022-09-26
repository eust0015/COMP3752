using System;
using System.Collections.Generic;
using System.Linq;
using UI.Abilities;
using UnityEngine;

namespace UI.AbilityInventory
{
    [Serializable]
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

        public void Display(List<Ability> abilities)
        {
            Clear();

            foreach (Ability ability in abilities)
            {
                AddAbility(ability);
            }
        }
        
        public void AddAbility(Ability ability)
        {
            InventoryAbilityUI inventoryAbility = InventoryAbilitiesList.FirstOrDefault(p => p.Ability.GetType() == ability.GetType());
            
            // If the ability was found in the list
            if (inventoryAbility != null)
            {
                inventoryAbility.Ability.Quantity += 1;
            }
            else // If the ability was not found in the list
            {
                inventoryAbility = Instantiate(InventoryAbilityPrefab, AbilityContainer);
                inventoryAbility.Initialise(ability);
                InventoryAbilitiesList.Add(inventoryAbility);
                inventoryAbility.OnDestroyed += RemoveAbilityFromList;
            }
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