using UI.Items;
using UnityEngine;

namespace UI.Abilities
{
    [System.Serializable]
    public abstract class Ability : Item
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
    }
}