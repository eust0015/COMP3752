using System;
using System.Collections.Generic;
using Audio;
using TMPro;
using UI.Items;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    [Serializable]
    public class ShopUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image picture;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private Transform itemDetailedContainer;
        [SerializeField] private ShopItemUI shopItemPrefab;
        [SerializeField] private List<ShopItemUI> shopItemsList;

        public delegate void Destroyed(ShopUI thisObject);
        public event Destroyed OnDestroyed;
        
        public TMP_Text Description
        {
            get => description;
            private set => description = value;
        }

        public Image Picture
        {
            get => picture;
            private set => picture = value;
        }

        public TMP_Text Title
        {
            get => title;
            private set => title = value;
        }

        public Transform ItemContainer
        {
            get => itemContainer;
            private set => itemContainer = value;
        }

        public Transform ItemDetailedContainer
        {
            get => itemDetailedContainer;
            private set => itemDetailedContainer = value;
        }

        public ShopItemUI ShopItemPrefab
        {
            get => shopItemPrefab;
            private set => shopItemPrefab = value;
        }
        
        public List<ShopItemUI> ShopItemsList
        {
            get
            {
                if (shopItemsList == null)
                    shopItemsList = new List<ShopItemUI>();
                return shopItemsList;
            }
            private set => shopItemsList = value;
        }

        private void OnEnable()
        {
            PauseGame();
            transform.SetAsFirstSibling();
            FMODMusicSource musicSource = GetComponent<FMODMusicSource>();
            musicSource.PlayMusic();
        }

        public void Display(List<Item> items)
        {
            Clear();

            foreach (Item item in items)
            {
                AddItem(item);
            }
        }
        
        private void AddItem(Item item)
        {
            ShopItemUI shopItem = Instantiate(ShopItemPrefab, ItemContainer);
            shopItem.Initialise(item, ItemDetailedContainer);
            ShopItemsList.Add(shopItem);
        }

        public void Clear()
        {
            foreach (ShopItemUI menuItem in ShopItemsList)
            {
                Destroy(menuItem.gameObject);
            }
            ShopItemsList.Clear();
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
        }
        private void ResumeGame()
        {
            Time.timeScale = 1;
        }
        
        public void Close()
        { 
            Clear();
            ResumeGame();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
        
    }
}