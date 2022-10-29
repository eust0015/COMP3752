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
        [SerializeField] private bool isInRange;
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

        public bool IsInRange
        {
            get => isInRange;
            private set => isInRange = value;
        }

        public void OnInteractKey()
        {
            if(!IsInRange)
                return;
                
            if (ActiveEvent == null)
                Display();
        }

        public void OnEventClosed(EventUI closedEvent)
        {
            bool optionWasChosen = closedEvent.OptionWasChosen;
            ActiveEvent.OnDestroyed -= OnEventClosed;
            ActiveEvent = null;
            if (optionWasChosen)
                Destroy(gameObject);
        }
        
        public void Display()
        {
            if (ActiveEvent != null)
                return;

            ActiveEvent = Instantiate(EventPrefab, UICanvas.transform);
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