using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class PlayerAttackController : MonoBehaviour
{
    public Weapon currentWeapon;
    public RangedWeapon ranged;
    public MeleeWeapon melee;
    private GameObject pivot;

    private float currentCd;

    private PlayerStats _s;
    private AttackController _a;
    private playerAnimation _p;
    private Rigidbody2D _rb2d;
    private playerMovement _pm;
    private Random _rand;
    private int _id = 0;

    private float angle = 0;

    private void Awake()
    {
        pivot = transform.GetChild(1).gameObject;
        _a = GetComponent<AttackController>();
        _s = GetComponent<PlayerStats>();
        _p = transform.GetChild(0).GetComponent<playerAnimation>();
        _pm = GetComponent<playerMovement>();
        _rb2d = GetComponent<Rigidbody2D>();
        _rand = new Random();
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
            StartCoroutine(_p.AttackAnim());
            angle = (Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg) - 90;
            //angle = Mathf.Acos(dir.x) * Mathf.Rad2Deg;
            switch (currentWeapon.GetType().Name)
            {
                case "RangedWeapon":
                    ProjectileAttack(ranged.projectiles);
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

    private void ProjectileAttack(List<Projectile> proj)
    {
        foreach (var _p in proj)
        {
            _a.RequestProjectile(_p);
        }
    }

    private void MeleeAttack(List<Hitbox> data)
    {
        if (!melee.multihit)
        {
            _id++;
            foreach (Hitbox _h in data)
            {
                float dmg = (_s.baseAtk + _s.atkModifier) * _s.atkMultiplier;
                int r = _rand.Next(99);
                if (r < _s.critChance) dmg *= (1 + _s.critDamage);
                _a.RequestHitbox(_h, Mathf.FloorToInt(dmg),_id, angle);
                var _m = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)).normalized * _h.momentum;
                Debug.Log(_m);
                _pm.xspeed += _m.x;
                _pm.yspeed += -_m.y;
            }
        }
        else
        {
            foreach (Hitbox _h in data)
            {
                _id++;
                _a.RequestHitbox(_h, Mathf.FloorToInt((_s.baseAtk + _s.atkModifier) * _s.atkMultiplier), _id, angle);
                var _m = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad),
                    Mathf.Sin(angle * Mathf.Deg2Rad)).normalized * (_h.momentum * 2);
                Debug.Log(_m);
                _pm.xspeed += _m.x;
                _pm.yspeed += -_m.y;

            }
        }
    }
}
