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
            set => critChance = value;
        }

        public Crit CritPrefab
        {
            get => critPrefab;
            private set => critPrefab = value;
        }

        public void DisplayCrit(Vector3 position)
        {
            Instantiate(CritPrefab, position, Quaternion.identity);
        }
        
    }
}