﻿using System;
using System.Collections.Generic;
using TMPro;
using UI.Items;
using UnityEngine;

namespace UI.Shop
{
    [Serializable]
    public class ShopUITest : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private ShopUI shopPrefab;
        [SerializeField] private ShopUI activeShop;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private List<Item> items;

        public Transform Canvas
        {
            get => canvas;
            private set => canvas = value;
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

        public TMP_Text ButtonText
        {
            get => buttonText;
            private set => buttonText = value;
        }

        public List<Item> Items
        {
            get => items;
            private set => items = value;
        }

        public void OnClick()
        {
            if (ActiveShop == null)
                Display();
            else
                Hide();
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
            
            ActiveShop = Instantiate(ShopPrefab, Canvas);
            ActiveShop.Display(items);
            ButtonText.text = "Hide";
        }

        public void Hide()
        {
            if (ActiveShop == null)
                return;
            
            Destroy(ActiveShop.gameObject);
            ActiveShop = null;
            ButtonText.text = "Show";
        }
        
    }
}