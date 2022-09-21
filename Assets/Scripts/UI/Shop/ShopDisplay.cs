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
            get
            {
                if (uICanvas == null)
                    uICanvas = GameObject.Find("UICanvas");
                return uICanvas;
            }
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
            Debug.Log("Registered");
            if(!IsInRange)
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
        
            Potion potion1 = new Potion
            {
                Name = "Item1Name",
                Description = "Item1Description",
                Price = 1,
                Quantity = 1
            };

            Potion potion2 = new Potion
            {
                Name = "Item2Name",
                Description = "Item2Description",
                Price = 2,
                Quantity = 1
            };
            
            Potion potion3 = new Potion
            {
                Name = "Item3Name",
                Description = "Item3Description",
                Price = 3,
                Quantity = 1
            };

            Items = new List<Item>
            {
                potion1,
                potion2,
                potion3,
            };
            
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