using System.Collections;
using UnityEngine;

namespace Player.Relics
{
    public class HeartSteal : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(DestroyCoroutine());
        }
        
        IEnumerator DestroyCoroutine()
        {
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }
}