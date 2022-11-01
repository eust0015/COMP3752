using Player;
using UnityEngine;

namespace UI.Effect
{
    public class EffectAddBubbleBarrierToPlayer : EffectUI
    {
        [SerializeField] protected PlayerBubbleBarrier bubbleBarrierPrefab;

        public override void DoEffect()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return;
            
            PlayerBubbleBarrier bubbleBarrier = Instantiate(bubbleBarrierPrefab, player.transform);
        }
    }
}