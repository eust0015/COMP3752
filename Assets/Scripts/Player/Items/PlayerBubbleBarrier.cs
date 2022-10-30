using Audio;
using UnityEngine;

namespace Player
{
    public class PlayerBubbleBarrier : MonoBehaviour
    {
        [SerializeField] public FMODAudioSource popSound;
        public void PopBubble()
        { 
            popSound.PlaySound();
            Destroy(gameObject);
        }
    }
}