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
        
            EventOptionHealth option1 = new EventOptionHealth
            {
                Title = "Health Option",
                Description = "Restore health",
            };

            EventOptionPollen option2 = new EventOptionPollen
            {
                Title = "Pollen Option",
                Description = "Receive Pollen",
            };
            
            EventOptionRelic option3 = new EventOptionRelic
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