using UnityEngine;

namespace Player.Relics
{
    public class PlayerHeartStealer : MonoBehaviour
    {
        [SerializeField] private int heartStealChance;
        [SerializeField] private HeartSteal heartStealPrefab;

        public int HeartStealChance
        {
            get => heartStealChance;
            private set => heartStealChance = value;
        }

        public HeartSteal HeartStealPrefab
        {
            get => heartStealPrefab;
            private set => heartStealPrefab = value;
        }
        
        public void DisplayHeartSteal(Vector3 position, Quaternion rotation)
        {
            Instantiate(HeartStealPrefab, position, rotation);
        }
    }
}