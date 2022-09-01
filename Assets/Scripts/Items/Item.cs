using UnityEngine;

namespace Items
{
    public abstract class Item
    {
        protected string description;
        protected Sprite icon;
        protected string name;
        protected int price;
        protected int quantity;
        
        public delegate void QuantityChanged();
        public event QuantityChanged OnQuantityChanged;
        public virtual string Description { get; set; }
        public virtual Sprite Icon { get; set; }
        public virtual string Name { get; set; }
        public virtual int Price { get; set; }
        public virtual int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnQuantityChanged?.Invoke();
            } 
        }
        public abstract void Buy();
        public abstract void Sell();
        public abstract void Use();
    }
}

