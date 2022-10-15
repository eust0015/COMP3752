using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Event
{
    [Serializable]
    public class EventDisplay : MonoBehaviour
    {
        [SerializeField] private GameObject uICanvas;
        [SerializeField] private EventUI eventPrefab;
        [SerializeField] private EventUI activeEvent;
        [SerializeField] private List<EventOption> eventOptions;
        [SerializeField] private bool isInRange;
        [SerializeField] private EventOptionFillAllHearts option1;
        [SerializeField] private EventOptionOneMaxHeartCapacity option2;
        [SerializeField] private EventOptionTwoMaxHeartCapacity option3;
        public GameObject UICanvas
        {
            get => (uICanvas == null ? GameObject.Find("UICanvas") : uICanvas);
            private set => uICanvas = value;
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

        public List<EventOption> EventOptions
        {
            get => eventOptions;
            private set => eventOptions = value;
        }

        public EventOptionFillAllHearts Option1
        {
            get => option1;
            private set => option1 = value;
        }

        public EventOptionOneMaxHeartCapacity Option2
        {
            get => option2;
            private set => option2 = value;
        }

        public EventOptionTwoMaxHeartCapacity Option3
        {
            get => option3;
            private set => option3 = value;
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
                
            if (ActiveEvent == null)
                Display();
        }

        public void OnEventClosed(EventUI closedEvent)
        {
            ActiveEvent.OnDestroyed -= OnEventClosed;
            ActiveEvent = null;
        }
        
        public void Display()
        {
            if (ActiveEvent != null)
                return;

            EventOptions = new List<EventOption>
            {
                Option1,
                Option2,
                Option3,
            };
            
            ActiveEvent = Instantiate(EventPrefab, UICanvas.transform);
            ActiveEvent.Display(EventOptions);
            ActiveEvent.OnDestroyed += OnEventClosed;
        }

        public void Hide()
        {
            if (ActiveEvent == null)
                return;
            
            Destroy(ActiveEvent.gameObject);
            ActiveEvent = null;
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