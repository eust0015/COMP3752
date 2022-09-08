using System;
using Statistics;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private Health health;

        public Health Health { get; set; }
    }
}