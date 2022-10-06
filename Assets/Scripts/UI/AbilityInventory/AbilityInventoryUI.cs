﻿using System;
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
        [SerializeField] public MeleeAttackAbility meleeAttackTemplate;
        [SerializeField] public DashAbility dashTemplate;
        [SerializeField] public RangedAttackAbility rangedAttackTemplate;
        
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

        public MeleeAttackAbility MeleeAttackTemplate
        {
            get => meleeAttackTemplate;
            private set => meleeAttackTemplate = value;
        }

        public DashAbility DashTemplate
        {
            get => dashTemplate;
            private set => dashTemplate = value;
        }

        public RangedAttackAbility RangedAttackTemplate
        {
            get => rangedAttackTemplate;
            private set => rangedAttackTemplate = value;
        }

        public void OnEnable()
        {
            List<Ability> abilities = new List<Ability>
            {
                MeleeAttackTemplate,
                DashTemplate
            };
            Display(abilities);
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