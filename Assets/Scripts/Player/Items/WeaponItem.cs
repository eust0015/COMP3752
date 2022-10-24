using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : PlayerItem
{
    private Weapon _weapon;
    private RangedWeapon _ranged;
    private MeleeWeapon _melee;

    private PlayerAttackController _p;

    private void Awake()
    {
        _p = GetComponent<PlayerAttackController>();
        ApplyWeapon();
        StatModifiers();
    }
    
    private void ApplyWeapon()
    {
        if (_weapon.GetType().Name == "MeleeWeapon")
        {
            _p.currentWeapon = _melee;
            _p.melee = _melee;
        }
        else
        {
            _p.currentWeapon = _ranged;
            _p.ranged = _ranged;
        }
    }
}
