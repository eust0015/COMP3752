using UnityEngine;

namespace Player.Relics
{
    public class PlayerThoroughForage : MonoBehaviour
    {
        [SerializeField] private int critChance;
        [SerializeField] private ThoroughForage thoroughForagePrefab;

        public int CritChance
        {
            get => critChance;
            private set => critChance = value;
        }

        public ThoroughForage ThoroughForagePrefab
        {
            get => thoroughForagePrefab;
            private set => thoroughForagePrefab = value;
        }

        public void DisplayThoroughForage(Vector3 position, Quaternion rotation)
        {
            Instantiate(ThoroughForagePrefab, position, rotation);
        }
    }
}