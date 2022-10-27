using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerBubbleBarrier : MonoBehaviour
    {
        public void OnEnable() => StartCoroutine(HideCoroutine());

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSecondsRealtime(20);
            Hide();
        }

        public void Hide() => Destroy(gameObject);
    }
}