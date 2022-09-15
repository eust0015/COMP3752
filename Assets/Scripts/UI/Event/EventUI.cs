using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    [Serializable]
    public class EventUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image picture;
        [SerializeField] private TMP_Text title;
        [SerializeField] private Transform optionContainer;
        [SerializeField] private EventOptionUI optionPrefab;
        [SerializeField] private List<EventOptionUI> eventOptionsList;

        public TMP_Text Description
        {
            get => description;
            set => description = value;
        }

        public Image Picture
        {
            get => picture;
            set => picture = value;
        }

        public TMP_Text Title
        {
            get => title;
            set => title = value;
        }

        public Transform OptionContainer
        {
            get => optionContainer;
            set => optionContainer = value;
        }

        public EventOptionUI OptionPrefab
        {
            get => optionPrefab;
            set => optionPrefab = value;
        }

        public List<EventOptionUI> EventOptionsList
        {
            get
            {
                if (eventOptionsList == null)
                    eventOptionsList = new List<EventOptionUI>();
                return eventOptionsList;
            }
            private set => eventOptionsList = value;
        }
        
        public void Display(List<EventOption> options)
        {
            Clear();

            foreach (EventOption item in options)
            {
                AddOption(item);
            }
        }
        
        private void AddOption(EventOption option)
        {
            EventOptionUI shopItem = Instantiate(OptionPrefab, OptionContainer);
            shopItem.Initialise(option);
            EventOptionsList.Add(shopItem);
        }

        public void Clear()
        {
            foreach (EventOptionUI menuOption in EventOptionsList)
            {
                Destroy(menuOption.gameObject);
            }
            EventOptionsList.Clear();
        }
    }
}