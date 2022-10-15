using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Event
{
    [Serializable]
    public class EventOptionDetailedUI : MonoBehaviour
    {
        [SerializeField] private EventOption option;
        [SerializeField] private TMP_Text title;
        [SerializeField] private TMP_Text description;
        [SerializeField] private Image icon;

        public delegate void Destroyed(EventOptionDetailedUI thisObject);
        public event Destroyed OnDestroyed;
        
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

        public void Initialise(EventOption setOption)
        {
            Option = setOption;
            UpdateTitle();
            UpdateDescription();
            UpdateSprite();
        }

        public void UpdateTitle() => Title.text = Option.Title;
        public void UpdateDescription() => Description.text = Option.Description;
        public void UpdateSprite() => Icon.sprite = Option.Icon;
        private void OnDestroy() => OnDestroyed?.Invoke(this);
    }
}