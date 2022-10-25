using UnityEngine;
using UnityEngine.Events;

namespace UI.UnityEvents
{
    public class OnDestroyEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent destroyEvent;

        public UnityEvent DestroyEvent
        {
            get => destroyEvent;
            set => destroyEvent = value;
        }

        private void OnDestroy()
        {
            DestroyEvent.Invoke();
        }
    }
}