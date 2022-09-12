using System;
using Items;
using UI.Items;
using UnityEngine;

namespace UI.Shop
{
    [Serializable]
    public class MouseOverShopItemUI : MonoBehaviour
    {
        [SerializeField] private ShopItemDetailedUI mouseOverPrefab;
        [SerializeField] private ShopItemDetailedUI activeMouseOver;
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