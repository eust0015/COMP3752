using System.Collections;
using System.Collections.Generic;
using Statistics;
using Unity.VisualScripting;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private int baseAtk;
    private int currentAtk;

    [SerializeField]
    private bool showHitboxes;

    [SerializeField] private GameObject hBox;

    public void RequestHitbox(Hitbox _h)
    {
        var box = Instantiate(hBox);
        box.transform.position = new Vector3(_h.relativePos.x, _h.relativePos.y, 0) + transform.position;
        box.transform.localScale = new Vector3(_h.dimX, _h.dimY, 1);

        var hit = box.GetComponent<HitBoxObject>();
        hit.timer = _h.duration;
        hit.damage = _h.damage;
        hit.owner = this;
        hit.showHitbox = showHitboxes;

    }

    public void RequestAttack(Health h, int damage)
    {
        h.DecreaseValue(damage);
    }
}
