using UnityEngine;

namespace UI.Items
{
    [System.Serializable]
    public class Item
    {
        [SerializeField] protected ItemType type;
        [SerializeField][TextArea] protected string description;
        [SerializeField] protected Sprite icon;
        [SerializeField] protected string name;
        [SerializeField] protected int price;
        [SerializeField] protected int quantity;
        
        public delegate void QuantityChanged();
        public event QuantityChanged OnQuantityChanged;

        public ItemType Type
        {
            get => type;
            protected set => type = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public Sprite Icon
        {
            get => icon;
            set => icon = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public int Price
        {
            get => price;
            set => price = value;
        }

        public virtual int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnQuantityChanged?.Invoke();
            } 
        }

        public virtual void Buy()
        {
        }

        public virtual void Sell()        
        {
        }
        
        public virtual void Use()
        {
        }
    }
}

