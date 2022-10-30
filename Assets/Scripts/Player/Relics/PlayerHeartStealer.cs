using UnityEngine;

namespace Player.Relics
{
    public class PlayerHeartStealer : MonoBehaviour
    {
        [SerializeField] private int heartStealChance;
        [SerializeField] private int heartStealAmount;
        [SerializeField] private HeartSteal heartStealPrefab;

        public int HeartStealChance
        {
            get => heartStealChance;
            set => heartStealChance = value;
        }

        public int HeartStealAmount
        {
            get => heartStealAmount;
            set => heartStealAmount = value;
        }

        public HeartSteal HeartStealPrefab
        {
            get => heartStealPrefab;
            private set => heartStealPrefab = value;
        }
        
        public void DisplayHeartSteal(Vector3 position)
        {
            Instantiate(HeartStealPrefab, position, Quaternion.identity);
        }
    }
}