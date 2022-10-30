using Audio;
using UnityEngine;

namespace Player.Relics
{
    public class PlayerCritter : MonoBehaviour
    {
        [SerializeField] private int critChance;
        [SerializeField] private Crit critPrefab;
        public int CritChance
        {
            get => critChance;
            private set => critChance = value;
        }

        public Crit CritPrefab
        {
            get => critPrefab;
            private set => critPrefab = value;
        }

        public void DisplayCrit(Vector3 position, Quaternion rotation)
        {
            Instantiate(CritPrefab, position, rotation);
        }
        
    }
}