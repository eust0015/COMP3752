using System;
using Items;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShopItemUI :MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private TMP_Text itemName;
        [SerializeField] private TMP_Text description;
        [SerializeField] private TMP_Text price;
        [SerializeField] private Image icon;
        
        public Item Item
        {
            get => item;
            private set => item = value;
        }

        public void Initialise(Item setItem)
        {
            Item = setItem;
            itemName.text = Item.Name;
            description.text = Item.Description;
            price.text = "$" + Item.Price;
            icon.sprite = Item.Icon;
        }
    }
}