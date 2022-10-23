using System;
using UnityEngine;
using System.Collections.Generic;

namespace UI.Dialogue
{
    [Serializable]
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] private DialogueUI dialoguePrefab;
        [SerializeField] private DialogueUI activeDialogue;
        [SerializeField] private bool isInRange;
        [SerializeField][TextArea(1,5)] private List<string> dialogueList;
        [SerializeField] private int dialogueIndex;
        
        public DialogueUI DialoguePrefab
        {
            get => dialoguePrefab;
            private set => dialoguePrefab = value;
        }

        public DialogueUI ActiveDialogue
        {
            get => activeDialogue;
            private set => activeDialogue = value;
        }
        
        public bool IsInRange
        {
            get => isInRange;
            private set => isInRange = value;
        }

        public List<string> DialogueList
        {
            get => dialogueList;
            private set => dialogueList = value;
        }

        public int DialogueIndex
        {
            get => dialogueIndex;
            private set => dialogueIndex = value;
        }

        public void Display()
        {
            if (ActiveDialogue != null)
                return;
            
            ActiveDialogue = Instantiate(DialoguePrefab, transform);
            ActiveDialogue.Initialise(DialogueList, DialogueIndex);
            SubscribeToEvents(ActiveDialogue);
        }

        public void SubscribeToEvents(DialogueUI dialogue)
        {
            dialogue.OnDestroyed += UnsubscribeToEvents;
            dialogue.OnDialogueIndexChanged += OnDialogueIndexChanged;
        }

        public void UnsubscribeToEvents(DialogueUI dialogue)
        {
            dialogue.OnDestroyed -= UnsubscribeToEvents;
            dialogue.OnDialogueIndexChanged -= OnDialogueIndexChanged;
        }
        
        public void Hide()
        {
            if (ActiveDialogue == null)
                return;
            
            Destroy(ActiveDialogue.gameObject);
            ActiveDialogue = null;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                IsInRange = true;
                Display();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                IsInRange = false;
                Hide();
            }
        }

        private void OnDialogueIndexChanged(int index) => DialogueIndex = index;
    }
}