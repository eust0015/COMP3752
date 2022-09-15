using System;
using System.Collections;
using UnityEngine;

namespace UI.Messages
{
    [Serializable]
    public class MessageUI : MonoBehaviour
    {
        public void OnEnable() => StartCoroutine(HideCoroutine());

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(5);
            Hide();
        }

        public void Hide() => Destroy(gameObject);
        
    }
}