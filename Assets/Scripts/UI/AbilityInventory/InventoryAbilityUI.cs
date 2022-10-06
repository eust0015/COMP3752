using System;
using TMPro;
using UI.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AbilityInventory
{
    [Serializable]
    public class InventoryAbilityUI : MonoBehaviour 
    {
        [SerializeField] private Ability ability;
        [SerializeField] private Image icon;
        [SerializeField] private Transform mouseOverContainer;
        [SerializeField] private InventoryAbilityDetailedUI mouseOverPrefab;
        [SerializeField] private InventoryAbilityDetailedUI activeMouseOver;

        public delegate void Destroyed(InventoryAbilityUI thisObject);
        public event Destroyed OnDestroyed;
        public Ability Ability
        {
            get => ability;
            private set => ability = value;
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

        public InventoryAbilityDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public InventoryAbilityDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }

        public void Initialise(Ability setAbility)
        {
            Ability = setAbility;
            SubscribeToEvents();
            UpdateLevel();
            UpdateSprite();
            HideDetailedDescription();
        }

        private void SubscribeToEvents()
        {
            if (Ability == null)
                return;
            
            Ability.OnLevelChanged += UpdateLevel;
        }

        private void UnsubscribeFromEvents()
        {
            if (Ability == null)
                return;
            
            Ability.OnLevelChanged -= UpdateLevel;
        }

        public void UpdateLevel() {}

        public void UpdateSprite() => icon.sprite = Ability.Icon;
        
        public void Use()
        {
            Ability.Use();
        }

        public void ShowDetailedDescription()
        {
            activeMouseOver = Instantiate(mouseOverPrefab, MouseOverContainer);
            activeMouseOver.Initialise(Ability);
        }

        public void HideDetailedDescription()
        {
            if (activeMouseOver == null)
                return;
            
            Destroy(activeMouseOver.gameObject);
            activeMouseOver = null;    
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
            UnsubscribeFromEvents();
        }
    }
}