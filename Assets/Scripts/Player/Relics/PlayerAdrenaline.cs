using UnityEngine;

namespace Player.Relics
{
    public class PlayerAdrenaline : MonoBehaviour
    {
        [SerializeField] private int critChance;
        [SerializeField] private int amountHealthShouldBeBelow;

        public int CritChance
        {
            get => critChance;
            set => critChance = value;
        }

        public int AmountHealthShouldBeBelow
        {
            get => amountHealthShouldBeBelow;
            set => amountHealthShouldBeBelow = value;
        }
    }
}