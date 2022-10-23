using FMODUnity;
using UnityEngine;

namespace Audio
{
    public class FMODAudioSource : MonoBehaviour
    {
        [SerializeField] public FMODUnity.EventReference sound;

        public void PlaySound()
        {
            RuntimeManager.PlayOneShot(sound);
        }
    }
}