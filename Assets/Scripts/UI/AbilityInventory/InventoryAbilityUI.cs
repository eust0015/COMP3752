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
        [SerializeField] protected Ability ability;
        [SerializeField] protected Image icon;
        [SerializeField] protected Transform mouseOverContainer;
        [SerializeField] protected InventoryAbilityDetailedUI mouseOverPrefab;
        [SerializeField] protected InventoryAbilityDetailedUI activeMouseOver;

        public delegate void Destroyed(InventoryAbilityUI thisObject);
        public event Destroyed OnDestroyed;
        public Ability Ability
        {
            get => ability;
            protected set => ability = value;
        }

        public Image Icon
        {
            get => icon;
            protected set => icon = value;
        }

        public Transform MouseOverContainer
        {
            get => mouseOverContainer;
            protected set => mouseOverContainer = value;
        }

        public InventoryAbilityDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            protected set => mouseOverPrefab = value;
        }

        public InventoryAbilityDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            protected set => activeMouseOver = value;
        }

        public void Initialise(Ability setAbility)
        {
            Ability = setAbility;
            SubscribeToEvents();
            UpdateLevel();
            UpdateSprite();
            HideDetailedDescription();
        }

        protected void SubscribeToEvents()
        {
            if (Ability == null)
                return;
            
            Ability.OnLevelChanged += UpdateLevel;
        }

        protected void UnsubscribeFromEvents()
        {
            if (Ability == null)
                return;
            
            Ability.OnLevelChanged -= UpdateLevel;
        }

        public void UpdateLevel() {}

        public void UpdateSprite() => icon.sprite = Ability.Icon;
        
        public virtual void Use()
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

        protected void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
            UnsubscribeFromEvents();
        }
    }
}