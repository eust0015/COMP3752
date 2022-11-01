using UnityEngine;

namespace Player.Relics
{
    public class PlayerThoroughForage : MonoBehaviour
    {
        [SerializeField] private int critChance;
        [SerializeField] private GameObject thoroughForagePrefab;
        [SerializeField] private Vector3 positionOffset;
        
        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public GameObject ThoroughForagePrefab
        {
            get => thoroughForagePrefab;
            private set => thoroughForagePrefab = value;
        }

        public Vector3 PositionOffset
        {
            get => positionOffset;
            set => positionOffset = value;
        }

        public void DisplayThoroughForage(Vector3 position)
        {
            Instantiate(ThoroughForagePrefab, position + PositionOffset, Quaternion.identity);
        }
    }
}