﻿using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class ShopUITest : MonoBehaviour
    {
        [SerializeField] private List<Item> items;
        private void OnEnable()
        {
        //Test
        
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
            
            Potion potion4 = new Potion
            {
                Name = "Item4Name",
                Description = "Item4Description",
                Price = 4,
                Quantity = 1
            };

            items = new List<Item>
            {
                potion1,
                potion2,
                potion3,
                potion4,
            };

            ShopUI shop = transform.GetComponent<ShopUI>();
            shop.Display(items);
        }
    }
}