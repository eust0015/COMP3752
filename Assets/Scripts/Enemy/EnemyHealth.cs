using System;
using Statistics;
using UnityEngine;

namespace Enemy
{
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