using System;
using UI.Statistics;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Health health;

        public Health Health
        {
            get => health;
            set => health = value;
        }
    }
}