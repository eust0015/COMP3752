using System;
using UnityEngine;

namespace UI.Dialogue
{
    [Serializable]
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] private Transform dialoguePrefab;
        [SerializeField] private Transform activeDialogue;
        [SerializeField] private bool isInRange;

        public Transform DialoguePrefab
        {
            get => dialoguePrefab;
            private set => dialoguePrefab = value;
        }

        public Transform ActiveDialogue
        {
            get => activeDialogue;
            private set => activeDialogue = value;
        }
        
        public bool IsInRange
        {
            get => isInRange;
            private set => isInRange = value;
        }

        public void Display()
        {
            if (ActiveDialogue != null)
                return;
            
            ActiveDialogue = Instantiate(DialoguePrefab, transform);
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
    }
}