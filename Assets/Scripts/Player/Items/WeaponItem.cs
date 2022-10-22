using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : PlayerItem
{
    private Weapon _weapon;

    private void Awake()
    {
        ApplyWeapon();
        StatModifiers();
    }
    
    private void ApplyWeapon()
    {
        throw new NotImplementedException();
    }
}
