using UnityEngine;

namespace Player.Relics
{
    public class PlayerHeartStealer : MonoBehaviour
    {
        [SerializeField] private int heartStealChance;
        [SerializeField] private int heartStealAmount;
        [SerializeField] private GameObject heartStealPrefab;
        [SerializeField] private Vector3 positionOffset;

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

        public GameObject HeartStealPrefab
        {
            get => heartStealPrefab;
            private set => heartStealPrefab = value;
        }

        public Vector3 PositionOffset
        {
            get => positionOffset;
            set => positionOffset = value;
        }

        public void DisplayHeartSteal(Vector3 position)
        {
            Instantiate(HeartStealPrefab, position + PositionOffset, Quaternion.identity);
        }
    }
}