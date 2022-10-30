using UnityEngine;

namespace Player.Relics
{
    public class PlayerAdrenaline : MonoBehaviour
    {
        [SerializeField] private int critChance;

        public int CritChance
        {
            get => critChance;
            private set => critChance = value;
        }
        
        
    }
}