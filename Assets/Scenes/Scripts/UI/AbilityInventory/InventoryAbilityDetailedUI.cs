using System;
using TMPro;
using UI.Abilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI.AbilityInventory
{
    [Serializable]
    public class InventoryAbilityDetailedUI :MonoBehaviour 
    {
        [SerializeField] private Ability ability;
        [SerializeField] private TMP_Text abilityName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;

        public delegate void Destroyed(InventoryAbilityDetailedUI thisObject);
        public event Destroyed OnDestroyed;
        public Ability Ability
        {
            get => ability;
            private set => ability = value;
        }

        public TMP_Text AbilityName
        {
            get => abilityName;
            private set => abilityName = value;
        }

        public TMP_Text Description
        {
            get => description;
            private set => description = value;
        }

        public Image Icon
        {
            get => icon;
            private set => icon = value;
        }

        public void Initialise(Ability setAbility)
        {
            Ability = setAbility;
            UpdateName();
            UpdateDescription();
            UpdateSprite();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (Ability == null)
                return;
        }

        private void UnsubscribeFromEvents()
        {
            if (Ability == null)
                return;
        }
        
        public void UpdateName() => abilityName.text = Ability.Name;
        public void UpdateDescription() => description.text = Ability.Description;
        public void UpdateSprite() => icon.sprite = Ability.Icon;

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
            OnDestroyed?.Invoke(this);
        }
    }
}