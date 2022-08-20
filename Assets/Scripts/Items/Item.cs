using UnityEngine;

namespace Items
{
    public abstract class Item
    {
        protected string description;
        protected Sprite icon;
        protected string name;
        protected int price;
        
        public virtual string Description { get; }
        public virtual Sprite Icon { get; }
        public virtual string Name { get; }
        public virtual int Price { get; }
        public abstract void Buy();
        public abstract void Sell();
        public abstract void Use();
    }
}

