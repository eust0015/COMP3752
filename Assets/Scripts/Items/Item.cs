using UnityEngine;

namespace Items
{
    public abstract class Item
    {
        protected string description;
        protected Sprite icon;
        protected string name;
        protected int price;
        
        public virtual string Description { get; set; }
        public virtual Sprite Icon { get; set; }
        public virtual string Name { get; set; }
        public virtual int Price { get; set; }
        public abstract void Buy();
        public abstract void Sell();
        public abstract void Use();
    }
}

