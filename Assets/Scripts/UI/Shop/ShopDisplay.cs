using System;
using System.Collections.Generic;
using UI.Items;
using UnityEngine;

namespace UI.Shop
{
    [Serializable]
    public class ShopDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject uICanvas;
        [SerializeField] private ShopUI shopPrefab;
        [SerializeField] private ShopUI activeShop;
        [SerializeField] private List<Item> items;
        [SerializeField] private bool isInRange;

        public GameObject UICanvas
        {
            get => (uICanvas == null ? GameObject.Find("UICanvas") : uICanvas);
            private set => uICanvas = value;
        }

        public ShopUI ShopPrefab
        {
            get => shopPrefab;
            private set => shopPrefab = value;
        }

        public ShopUI ActiveShop
        {
            get => activeShop;
            private set => activeShop = value;
        }

        public List<Item> Items
        {
            get => items;
            private set => items = value;
        }

        public bool IsInRange
        {
            get => isInRange;
            private set => isInRange = value;
        }

        public void OnInteractKey()
        {
            if (!IsInRange)
                return;

            if (ActiveShop == null)
                Display();
        }

        public void OnShopClosed(ShopUI shop)
        {
            ActiveShop.OnDestroyed -= OnShopClosed;
            ActiveShop = null;
        }

        public void Display()
        {
            if (ActiveShop != null)
                return;

            ActiveShop = Instantiate(ShopPrefab, UICanvas.transform);
            ActiveShop.Display(items);
            ActiveShop.OnDestroyed += OnShopClosed;
        }

        public void Hide()
        {
            if (ActiveShop == null)
                return;

            Destroy(ActiveShop.gameObject);
            ActiveShop = null;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                IsInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IsInRange = false;
            }
        }
    }
}