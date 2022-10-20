using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text dialogueText;
        [SerializeField] private TMP_Text hotKeyText;
        [SerializeField] private RectTransform backgroundTransform;
        [SerializeField] private RectTransform foregroundTransform;
        [SerializeField][TextArea] private List<string> dialogueList;
        [SerializeField] private string hotKey;
        [SerializeField] private int dialogueIndex;
        [SerializeField] private Vector2 backgroundPadding;
        [SerializeField] private Vector2 foregroundPadding;
        
        public delegate void DialogueIndexChanged(int newIndex);
        public event DialogueIndexChanged OnDialogueIndexChanged;
        public delegate void Destroyed(DialogueUI thisObject);
        public event Destroyed OnDestroyed;
        
        public TMP_Text DialogueText
        {
            get => dialogueText;
            private set => dialogueText = value;
        }

        public TMP_Text HotKeyText
        {
            get => hotKeyText;
            private set => hotKeyText = value;
        }

        public RectTransform BackgroundTransform
        {
            get => backgroundTransform;
            private set => backgroundTransform = value;
        }

        public RectTransform ForegroundTransform
        {
            get => foregroundTransform;
            private set => foregroundTransform = value;
        }

        public List<string> DialogueList
        {
            get => dialogueList;
            private set => dialogueList = value;
        }

        public string HotKey
        {
            get => hotKey;
            private set => hotKey = value;
        }

        public int DialogueIndex
        {
            get
            {
                return dialogueIndex;
            }
            private set
            {
                dialogueIndex = value;
                OnDialogueIndexChanged?.Invoke(dialogueIndex);
            }
        }

        public Vector2 BackgroundPadding
        {
            get => backgroundPadding;
            private set => backgroundPadding = value;
        }

        public Vector2 ForegroundPadding
        {
            get => foregroundPadding;
            private set => foregroundPadding = value;
        }

        public void Initialise(List<string> setDialogueList, int setCurrentIndex)
        {
            DialogueList = setDialogueList;
            DialogueIndex = setCurrentIndex;
            UpdateHotkey();
            UpdateDialogue();
        }

        private void NextDialogueIndex()
        {
            DialogueIndex = DialogueIndex == DialogueList.Count - 1 ? 0 : DialogueIndex + 1;
        }

        private void UpdateHotkey() => HotKeyText.SetText(HotKey);

        private void UpdateDialogue()
        {
            DialogueText.SetText(DialogueList[DialogueIndex]);
            
            // DialogueText.ForceMeshUpdate();
            // Vector2 textSize = DialogueText.GetRenderedValues(false);
            // ResizeRectTransform(BackgroundTransform, textSize + BackgroundPadding);
            // ResizeRectTransform(ForegroundTransform, textSize + ForegroundPadding);
            // LayoutRebuilder.MarkLayoutForRebuild(ForegroundTransform);
            // LayoutRebuilder.MarkLayoutForRebuild(BackgroundTransform);
            // LayoutRebuilder.MarkLayoutForRebuild((RectTransform) this.transform);
        }

        private void ResizeRectTransform(RectTransform setTransform, Vector2 size)
        {
            setTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            setTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
            setTransform.ForceUpdateRectTransforms();
        }

        public void DisplayNextDialogue(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            
            NextDialogueIndex();
            UpdateDialogue();

        }
        
        private void OnDestroy()
        {
            OnDestroyed?.Invoke(this);
        }
    }
}