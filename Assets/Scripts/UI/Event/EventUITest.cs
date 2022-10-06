using System;
using System.Collections.Generic;
using TMPro;
using UI.Items;
using UI.Shop;
using UnityEngine;

namespace UI.Event
{
    [Serializable]
    public class EventUITest : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private EventUI eventPrefab;
        [SerializeField] private EventUI activeEvent;
        [SerializeField] private TMP_Text buttonText;
        [SerializeField] private List<EventOption> eventOptions;
        
        public Transform Canvas
        {
            get => canvas;
            private set => canvas = value;
        }

        public EventUI EventPrefab
        {
            get => eventPrefab;
            private set => eventPrefab = value;
        }

        public EventUI ActiveEvent
        {
            get => activeEvent;
            private set => activeEvent = value;
        }

        public TMP_Text ButtonText
        {
            get => buttonText;
            private set => buttonText = value;
        }

        public List<EventOption> EventOptions
        {
            get => eventOptions;
            private set => eventOptions = value;
        }

        public void OnClick()
        {
            if (ActiveEvent == null)
                Display();
            else
                Hide();
        }
        
        public void Display()
        {
            if (ActiveEvent != null)
                return;
        
            EventOptionFillAllHearts option1 = new EventOptionFillAllHearts
            {
                Title = "Health Option",
                Description = "Restore health",
            };

            EventOptionOneMaxHeartCapacity option2 = new EventOptionOneMaxHeartCapacity
            {
                Title = "Pollen Option",
                Description = "Receive Pollen",
            };
            
            EventOptionTwoMaxHeartCapacity option3 = new EventOptionTwoMaxHeartCapacity
            {
                Title = "Relic Option",
                Description = "Receive a Relic",
            };

            EventOptions = new List<EventOption>
            {
                option1,
                option2,
                option3,
            };
            
            ActiveEvent = Instantiate(EventPrefab, Canvas);
            ActiveEvent.Display(eventOptions);
            ButtonText.text = "Hide";
        }

        public void Hide()
        {
            if (ActiveEvent == null)
                return;
            
            Destroy(ActiveEvent.gameObject);
            ActiveEvent = null;
            ButtonText.text = "Show";
        }
    }
}