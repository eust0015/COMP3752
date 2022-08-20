using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Statistics
{
    [Serializable]
    public abstract class Statistic
    {
        [SerializeField] protected string description;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected int maxValue;
        protected static int maxValueLimit = int.MaxValue;
        [SerializeField] protected string name;
        [SerializeField] protected int value;

        public delegate void MaxValueChanged();
        public delegate void ValueChanged();
        public event ValueChanged OnValueChanged;
        public event MaxValueChanged OnMaxValueChanged;
        
        public virtual string Description { get; }
        public virtual Sprite Icon { get; }
        public virtual int MaxValue
        {
            get => maxValue;
            private set
            {
                maxValue = value;
                OnMaxValueChanged?.Invoke();
            }
        }
        
        public virtual string Name { get; }

        public virtual int Value
        {
            get => value;
            private set
            {
                this.value = value;
                OnValueChanged?.Invoke();
            }
        }

        public virtual void IncreaseMaxValue(int amount)
        {
            MaxValue = (long)MaxValue + amount > maxValueLimit ? MaxValue : MaxValue + amount;
        }

        public virtual void DecreaseMaxValue(int amount)
        {
            MaxValue = MaxValue - amount < 0 ? 0 : MaxValue - amount;
        }
        
        public virtual void IncreaseValue(int amount)
        {
            Value = (long)Value + amount > MaxValue ? MaxValue : Value + amount;
        }
        
        public virtual void DecreaseValue(int amount)
        {
            Value = Value - amount < 0 ? 0 : Value - amount;
        }
        

        
    }
}