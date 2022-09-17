using System;
using UI.Items;
using UnityEngine;

namespace UI.Shop
{
    [Serializable]
    public class MouseOverShopItemUI : MonoBehaviour
    {
        [SerializeField] private ShopItemDetailedUI mouseOverPrefab;
        [SerializeField] private ShopItemDetailedUI activeMouseOver;

        public ShopItemDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public ShopItemDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }

        protected virtual void OnEnable()
        {
            Hide();
        }

        public virtual void Display(Item item)
        {
            activeMouseOver = Instantiate(mouseOverPrefab, transform);
            activeMouseOver.Initialise(item);
        }
        
        public virtual void Hide()
        {
            if (activeMouseOver == null)
                return;
            
            Destroy(activeMouseOver.gameObject);
            activeMouseOver = null;
        }
    }
}