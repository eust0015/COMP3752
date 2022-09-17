using System;
using UI.Items;
using UnityEngine;

namespace UI.Inventory
{
    [Serializable]
    public class MouseOverInventoryItemUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemDetailedUI mouseOverPrefab;
        [SerializeField] private InventoryItemDetailedUI activeMouseOver;

        public InventoryItemDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public InventoryItemDetailedUI ActiveMouseOver
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