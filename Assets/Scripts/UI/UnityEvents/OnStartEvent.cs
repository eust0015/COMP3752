using UnityEngine;
using UnityEngine.Events;

namespace UI.UnityEvents
{
    public class OnStartEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent startEvent;

        public UnityEvent StartEvent
        {
            get => startEvent;
            set => startEvent = value;
        }

        private void Start()
        {
            StartEvent.Invoke();
        }
    }
}