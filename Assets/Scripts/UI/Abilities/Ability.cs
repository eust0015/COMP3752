using UI.Items;
using UnityEngine;

namespace UI.Abilities
{
    [System.Serializable]
    public class Ability : Item
    {
        [SerializeField] protected int level;
        
        public delegate void LevelChanged();
        public event LevelChanged OnLevelChanged;
        
        public virtual int Level
        {
            get => level;
            set
            {
                level = value;
                OnLevelChanged?.Invoke();
            } 
        }

        public override void Buy()
        {
            throw new System.NotImplementedException();
        }

        public override void Sell()
        {
            throw new System.NotImplementedException();
        }

        public override void Use()
        {
            throw new System.NotImplementedException();
        }
    }
}