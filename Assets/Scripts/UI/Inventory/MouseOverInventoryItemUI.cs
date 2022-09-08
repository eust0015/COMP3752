using System;
using Items;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class MouseOverInventoryItemUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemDetailedUI mouseOverPrefab;
        [SerializeField] private InventoryItemDetailedUI activeMouseOver;
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