using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public Weapon currentWeapon;
    public RangedWeapon ranged;
    public MeleeWeapon melee;
    private GameObject pivot;

    private float currentCd;

    private PlayerStats _s;
    private AttackController _a;
    private int _id = 0;

    private float angle = 0;

    private void Awake()
    {
        pivot = transform.GetChild(1).gameObject;
        _a = GetComponent<AttackController>();
        _s = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (currentCd >= 0)
        {
            currentCd -= Time.deltaTime;
        }

        pivot.transform.position = transform.position;
    }

    public void Attack(Vector2 dir)
    {
        if (currentCd <= 0)
        {
            angle = (Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg) - 90;
            //angle = Mathf.Acos(dir.x) * Mathf.Rad2Deg;
            switch (currentWeapon.GetType().Name)
            {
                case "RangedWeapon":
                    ProjectileAttack(ranged.projectiles, dir);
                    break;
                case "MeleeWeapon":
                    MeleeAttack(melee.Data);
                    break;
                default:
                    Debug.LogWarning("Invalid Weapon Type: " + currentWeapon.GetType().Name);
                    break;
            }
            currentCd = currentWeapon.baseCd - (currentWeapon.baseCd / 100) * _s.cooldownReduction;
        }
    }

    private void ProjectileAttack(List<Projectile> proj, Vector2 lastDir)
    {
        
    }

    private void MeleeAttack(List<Hitbox> data)
    {
        if (!melee.multihit)
        {
            _id++;
            foreach (Hitbox _h in data)
            {
                _a.RequestHitbox(_h, Mathf.FloorToInt((_s.baseAtk + _s.atkModifier) * _s.atkMultiplier),_id, angle);
            }
        }
        else
        {
            foreach (Hitbox _h in data)
            {
                _id++;
                _a.RequestHitbox(_h, Mathf.FloorToInt((_s.baseAtk + _s.atkModifier) * _s.atkMultiplier), _id, angle);
            }
        }
    }
}
