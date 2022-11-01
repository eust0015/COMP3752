using Audio;
using UnityEngine;

namespace Player.Relics
{
    public class PlayerCritter : MonoBehaviour
    {
        [SerializeField] private int critChance;
        [SerializeField] private GameObject critPrefab;
        [SerializeField] private Vector3 positionOffset;
        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public GameObject CritPrefab
        {
            get => critPrefab;
            private set => critPrefab = value;
        }

        public Vector3 PositionOffset
        {
            get => positionOffset;
            set => positionOffset = value;
        }

        public void DisplayCrit(Vector3 position)
        {
            Instantiate(CritPrefab, position + PositionOffset, Quaternion.identity);
        }
        
    }
}