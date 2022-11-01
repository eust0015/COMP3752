using UnityEngine;

namespace UI.Effect
{
    public class EffectShowActivationOnPlayer : EffectUI
    {
        [SerializeField] private GameObject activatePrefab;
        [SerializeField] private Vector3 offsetFromPlayer;

        public GameObject ActivatePrefab
        {
            get => activatePrefab;
            set => activatePrefab = value;
        }

        public Vector3 OffsetFromPlayer
        {
            get => offsetFromPlayer;
            set => offsetFromPlayer = value;
        }

        public override void DoEffect()
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            if (player == null)
                return;

            Instantiate(ActivatePrefab, player);

        }
    }
}