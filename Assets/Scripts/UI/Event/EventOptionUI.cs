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
        [SerializeField] protected EventOption option;
        [SerializeField] protected TMP_Text title;
        [SerializeField] protected TMP_Text description;
        [SerializeField] protected Image icon;
        [SerializeField] protected List<InventoryAbilityUI> abilityList;
        [SerializeField] protected List<InventoryRelicUI> relicList;
        [SerializeField] protected List<InventoryItemUI> itemList;
        [SerializeField] protected List<EffectUI> effectList;
        [SerializeField] protected Transform mouseOverContainer;
        [SerializeField] protected List<Transform> activeMouseOverList;
        
        public EventOption Option
        {
            get => option;
            protected set => option = value;
        }
        
        public TMP_Text Title
        {
            get => title;
            protected set => title = value;
        }

        public TMP_Text Description
        {
            get => description;
            protected set => description = value;
        }

        public Image Icon
        {
            get => icon;
            protected set => icon = value;
        }

        public List<InventoryAbilityUI> AbilityList
        {
            get => abilityList;
            protected set => abilityList = value;
        }

        public List<InventoryRelicUI> RelicList
        {
            get => relicList;
            protected set => relicList = value;
        }

        public List<InventoryItemUI> ItemList
        {
            get => itemList;
            protected set => itemList = value;
        }

        public List<EffectUI> EffectList
        {
            get => effectList;
            set => effectList = value;
        }

        public Transform MouseOverContainer
        {
            get => mouseOverContainer;
            protected set => mouseOverContainer = value;
        }

        public List<Transform> ActiveMouseOverList
        {
            get => activeMouseOverList;
            protected set => activeMouseOverList = value;
        }

        public void Start()
        {
            UpdateDescription();
        }

        public void Initialise(EventOption setOption)
        {
            Option = setOption;
            UpdateDescription();
        }

        public virtual void Choose()
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
        public virtual void UpdateDescription() => Description.text = "<b>" + Option.Title + "</b>\n" + Option.Description;
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