using System;
using UI.Statistics;
using UnityEngine;

namespace Enemy
{
    [Serializable]
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private Health health;

        public Health Health
        {
            get => health;
            set => health = value;
        }
    }
}