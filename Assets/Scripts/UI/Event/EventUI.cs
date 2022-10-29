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
        [SerializeField] private bool optionWasChosen;

        public delegate void Destroyed(EventUI thisObject);
        public event Destroyed OnDestroyed;
        
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

        public bool OptionWasChosen
        {
            get => optionWasChosen;
            private set => optionWasChosen = value;
        }

        private void OnEnable()
        {
            PauseGame();
            transform.SetAsFirstSibling();
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
        
        private void PauseGame()
        {
            Time.timeScale = 0;
            GameManager.current.OnOnTimerPause();
        }
        private void ResumeGame()
        {
            Time.timeScale = 1;
            GameManager.current.OnOnTimerPlay();
        }
        
        public void Close(bool setOptionWasChosen)
        {
            OptionWasChosen = setOptionWasChosen;
            Clear();
            ResumeGame();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}