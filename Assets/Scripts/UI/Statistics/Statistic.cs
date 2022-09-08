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

        public delegate void MaxValueIncreased();
        public delegate void MaxValueDecreased();
        public delegate void ValueIncreased();
        public delegate void ValueDecreased();
        public event ValueIncreased OnValueIncreased;
        public event ValueDecreased OnValueDecreased;
        public event MaxValueIncreased OnMaxValueIncreased;
        public event MaxValueDecreased OnMaxValueDecreased;
        
        public virtual string Description { get; }
        public virtual Sprite Icon { get; }
        public virtual int MaxValue
        {
            get => maxValue;
            protected set => maxValue = value;
        }
        
        public virtual string Name { get; }

        public virtual int Value
        {
            get => value;
            protected set => this.value = value;
        }

        public virtual void IncreaseMaxValue(int amount)
        {
            MaxValue = (long)MaxValue + amount > maxValueLimit ? MaxValue : MaxValue + amount;
            OnMaxValueIncreased?.Invoke();
        }

        public virtual void DecreaseMaxValue(int amount)
        {
            MaxValue = MaxValue - amount < 0 ? 0 : MaxValue - amount;
            OnMaxValueDecreased?.Invoke();
        }
        
        public virtual void IncreaseValue(int amount)
        {
            Value = (long)Value + amount > MaxValue ? MaxValue : Value + amount;
            OnValueIncreased?.Invoke();
        }
        
        public virtual void DecreaseValue(int amount)
        {
            Value = Value - amount < 0 ? 0 : Value - amount;
            OnValueDecreased?.Invoke();
        }
        

        
    }
}