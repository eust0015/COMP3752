using System;
using System.Collections;
using UnityEngine;

namespace UI
{
    [Serializable]
    public abstract class MessageUI : MonoBehaviour
    {
        [SerializeField] private GameObject messagePrefab;
        [SerializeField] private GameObject activeMessage;
        protected virtual void OnEnable()
        {
            Hide();
        }

        public virtual void Display()
        {
            activeMessage = Instantiate(messagePrefab, transform);

            if (activeMessage == null)
                return;
            
            StartCoroutine(HideCoroutine());
        }

        protected IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(5);
            Hide();
        }

        protected virtual void Hide()
        {
            if (activeMessage == null)
                return;
            
            Destroy(activeMessage);
        }
    }
}