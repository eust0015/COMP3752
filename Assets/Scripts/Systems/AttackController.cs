using System.Collections;
using System.Collections.Generic;
using UI.Statistics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    private int baseAtk;
    private int currentAtk;
    private int curAtkID = 0;

    [SerializeField] public bool attackBasedOnTag;
    [SerializeField] public string tagToAttack;

    [SerializeField]
    private bool showHitboxes;

    [SerializeField] private GameObject hBox;
    [SerializeField] private GameObject projectile;

    public void RequestHitbox(Hitbox _h, int id = -1)
    {
        var box = Instantiate(hBox);
        box.transform.position = new Vector3(_h.relativePos.x, _h.relativePos.y, 0) + transform.position;
        box.transform.localScale = new Vector3(_h.dimX, _h.dimY, 1);

        var hit = box.GetComponent<HitBoxObject>();
        
        if (attackBasedOnTag)
        {
            hit.basedOnTag = true;
            hit._tag = tagToAttack;
        }
        
        hit.timer = _h.activeFrames * 60;
        hit.damage = _h.damage;
        hit.owner = this;
        hit.showHitbox = showHitboxes;
    }

    public void RequestMulti(List<Hitbox> _h, bool multihit = false)
    {
        foreach (Hitbox hit in _h)
        {
            
        }

        curAtkID++;
    }
    
    public void RequestProjectile(Projectile _p)
    {
        var proj = Instantiate(projectile);
        proj.transform.position = transform.position;
        proj.transform.eulerAngles = transform.eulerAngles;
        ProjectileObject obj = projectile.GetComponent<ProjectileObject>();
        obj.speed = _p.speed;
        obj.damage = _p.damage;
        obj.timer = _p.duration;
        obj.pierce = _p.pierce;
        obj.pierceCount = _p.pierceCount;
        proj.GetComponent<SpriteRenderer>().sprite = _p.sprite;
        proj.transform.rotation = transform.rotation;
        obj.owner = this;

        if (attackBasedOnTag)
        {
            obj.basedOnTag = true;
            obj._tag = tagToAttack;
        }
    }

    public void RequestAttack(Health h, int damage)
    {
        h.DecreaseValue(damage);
    }
}
