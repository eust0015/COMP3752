using System;
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

    public void RequestHitbox(Hitbox _h, int damage, int id = -1, float angle = 0)
    {
        GameObject box;
        box = Instantiate(hBox);
        
        
        box.transform.localPosition = new Vector3(_h.relativePos.x, _h.relativePos.y, 0) + transform.position;
        box.transform.localScale = new Vector3(_h.dimX, _h.dimY, 1);

        var hit = box.GetComponent<HitBoxObject>();
        
        if (attackBasedOnTag)
        {
            hit.basedOnTag = true;
            hit._tag = tagToAttack;
        }
        
        hit.timer = _h.activeFrames / 60;
        hit.damage = damage;
        hit.owner = this;
        hit.showHitbox = showHitboxes;
        hit.angle = angle;
        hit.knockback = _h.knockback;
    }

    public event Action onHit;
    public event Action onKill;
    public event Action onHurt;

    public void RequestProjectile(Projectile _p, int damage = 1, float angle = 0)
    {
        Debug.Log("request");
        var proj = Instantiate(projectile);
        ProjectileObject obj = projectile.GetComponent<ProjectileObject>();
        obj.owner = this;
        proj.transform.position = transform.position;
        if (CompareTag("Player"))
        {
            proj.transform.RotateAround(transform.position, Vector3.back, angle + _p.angle + 90);
        }
        else
        {
            proj.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + _p.angle);
        }
        obj.speed = _p.speed;
        obj.damage = damage;
        obj.timer = _p.duration;
        obj.pierce = _p.pierce;
        obj.pierceCount = _p.pierceCount;
        //proj.GetComponent<SpriteRenderer>().sprite = _p.sprite;
        //proj.transform.rotation = transform.rotation;


        if (attackBasedOnTag)
        {
            obj.basedOnTag = true;
            obj._tag = tagToAttack;
        }
    }

    public void RequestAttack(Health h, int damage)
    {
        h.DecreaseValue(damage);
        if(CompareTag("Player")) onHurt?.Invoke();
    }

    public void Kill()
    {
        Debug.Log("kill");
        onKill?.Invoke();
    }

    public void Hit()
    {
        Debug.Log("hit");
        onHit?.Invoke();
    }

    
}
