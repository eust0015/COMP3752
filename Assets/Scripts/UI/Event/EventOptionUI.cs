using System;
using System.Collections.Generic;
using TMPro;
using UI.AbilityInventory;
using UI.Effect;
using UI.Inventory;
using UI.ItemInventory;
using UI.RelicInventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    [Serializable]
    public class EventOptionUI : MonoBehaviour
    {
        [SerializeField] private EventOption option;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;
        [SerializeField] private List<InventoryAbilityUI> abilityList;
        [SerializeField] private List<InventoryRelicUI> relicList;
        [SerializeField] private List<InventoryItemUI> itemList;
        [SerializeField] private List<EffectUI> effectList;
        [SerializeField] private Transform mouseOverContainer;
        [SerializeField] private List<Transform> activeMouseOverList;
        
        public EventOption Option
        {
            get => option;
            private set => option = value;
        }
        
        public TMP_Text Title
        {
            get => title;
            private set => title = value;
        }

        public TMP_Text Description
        {
            get => description;
            private set => description = value;
        }

        public Image Icon
        {
            get => icon;
            private set => icon = value;
        }

        public List<InventoryAbilityUI> AbilityList
        {
            get => abilityList;
            private set => abilityList = value;
        }

        public List<InventoryRelicUI> RelicList
        {
            get => relicList;
            private set => relicList = value;
        }

        public List<InventoryItemUI> ItemList
        {
            get => itemList;
            private set => itemList = value;
        }

        public List<EffectUI> EffectList
        {
            get => effectList;
            set => effectList = value;
        }

        public Transform MouseOverContainer
        {
            get => mouseOverContainer;
            private set => mouseOverContainer = value;
        }

        public List<Transform> ActiveMouseOverList
        {
            get => activeMouseOverList;
            private set => activeMouseOverList = value;
        }

        public void Initialise(EventOption setOption)
        {
            Option = setOption;
            UpdateDescription();
        }

        public void Choose()
        {
            Option.Choose();
            foreach (var ability in AbilityList)
            {
                AbilityInventoryUI abilityInventory = FindObjectOfType<AbilityInventoryUI>();
                abilityInventory.AddAbility(ability);
            }
            foreach (var relic in RelicList)
            {
                RelicInventoryUI relicInventory = FindObjectOfType<RelicInventoryUI>();
                relicInventory.AddInventoryRelicUI(relic);
            }
            foreach (var item in ItemList)
            {
                ItemInventoryUI itemInventory = FindObjectOfType<ItemInventoryUI>();
                itemInventory.AddItem(item);
            }
            foreach (var effect in EffectList)
            {
                Instantiate(effect);
            }
            EventUI theEvent = FindObjectOfType<EventUI>();
            theEvent.Close(true);
        }
        
        public void UpdateTitle() => Title.text = Option.Title;
        public void UpdateDescription() => Description.text = Option.Description;
        public void UpdateSprite() => Icon.sprite = Option.Icon;
        
        public void ShowDetailedDescription()
        {
            if (ActiveMouseOverList == null)
                ActiveMouseOverList = new List<Transform>();
            else if (ActiveMouseOverList.Count > 0)
                return;

            foreach (var ability in AbilityList)
            {
                InventoryAbilityDetailedUI abilityDetailed = Instantiate(ability.MouseOverPrefab, MouseOverContainer);
                abilityDetailed.Initialise(ability.Ability);
                ActiveMouseOverList.Add(abilityDetailed.transform);
            }
            foreach (var relic in RelicList)
            {
                InventoryRelicDetailedUI relicDetailed = Instantiate(relic.MouseOverPrefab, MouseOverContainer);
                relicDetailed.Initialise(relic.Relic);
                ActiveMouseOverList.Add(relicDetailed.transform);
            }
            foreach (var item in ItemList)
            {
                InventoryItemDetailedUI itemDetailed = Instantiate(item.MouseOverPrefab, MouseOverContainer);
                itemDetailed.Initialise(item.Item);
                ActiveMouseOverList.Add(itemDetailed.transform);
            }
        }

        public void HideDetailedDescription()
        {
            if (ActiveMouseOverList == null)
                return;
            
            foreach (var detail in ActiveMouseOverList)
            {
                Destroy(detail.gameObject);
            }
            
            ActiveMouseOverList = null;    
        }

    }
}