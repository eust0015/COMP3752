using Player;
using UnityEngine;

namespace UI.Effect
{
    public class EffectAddBubbleBarrierToPlayer : MonoBehaviour
    {
        [SerializeField] protected PlayerBubbleBarrier bubbleBarrierPrefab;

        public void AddToPlayer()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
                return;
            
            PlayerBubbleBarrier bubbleBarrier = Instantiate(bubbleBarrierPrefab, player.transform);
            
        }
    }
}