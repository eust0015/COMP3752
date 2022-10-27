using UI.Abilities;
using UI.AbilityInventory;
using UnityEngine;

namespace UI.Effect
{
    public class EffectAddMeleeAttackToPlayer : MonoBehaviour
    {
        [SerializeField] protected Ability ability;
        
        public Ability Ability
        {
            get => ability;
            protected set => ability = value;
        }
        
        public void AddToPlayer()
        {
            InventoryAbilityUI abilityUI = GetComponent<InventoryAbilityUI>();
            if (abilityUI == null)
                return;
            Ability = abilityUI.Ability;
            
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return;

            WeaponItem abilityOnPlayer = player.AddComponent<WeaponItem>();
            abilityOnPlayer.itemName = Ability.Name;
            abilityOnPlayer.itemImage = Ability.Icon;
            GameManager.current.ItemObtained(abilityOnPlayer);
        }
    }
}