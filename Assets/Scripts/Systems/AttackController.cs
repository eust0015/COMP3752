using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private int baseAtk;
    private int currentAtk;

    [SerializeField] private GameObject hBox;

    public void RequestHitbox(Hitbox _h)
    {
        var box = Instantiate(hBox);
        
        box.transform.localPosition = new Vector3(_h.relativePos.x, _h.relativePos.y, 0);
        box.transform.localScale = new Vector3(_h.dimX, _h.dimY, 1);

        var hit = box.GetComponent<HitBoxObject>();
        hit.timer = _h.duration;
        hit.damage = _h.damage;
        hit.owner = this;

    }

    public void RequestAttack(Health h, int damage)
    {
        h.DecreaseValue(damage);
    }
}
