using System;
using UnityEngine;

namespace UI.Event
{
    [Serializable]
    public class MouseOverEventOptionUI : MonoBehaviour
    {
        [SerializeField] private EventOptionDetailedUI mouseOverPrefab;
        [SerializeField] private EventOptionDetailedUI activeMouseOver;

        public EventOptionDetailedUI MouseOverPrefab
        {
            get => mouseOverPrefab;
            private set => mouseOverPrefab = value;
        }

        public EventOptionDetailedUI ActiveMouseOver
        {
            get => activeMouseOver;
            private set => activeMouseOver = value;
        }
        
        protected virtual void OnEnable()
        {
            Hide();
        }

        public virtual void Display(EventOption option)
        {
            activeMouseOver = Instantiate(mouseOverPrefab, transform);
            activeMouseOver.Initialise(option);
        }
        
        public virtual void Hide()
        {
            if (activeMouseOver == null)
                return;
            
            Destroy(activeMouseOver.gameObject);
            activeMouseOver = null;
        }
    }
}