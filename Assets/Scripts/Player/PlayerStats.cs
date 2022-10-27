using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float baseAtk = 1;
    public float atkMultiplier = 1;
    public int atkModifier = 0;

    //Crit chance & dmg, 10 chance = 10% chance to crit, 1 dmg = +100% dmg on crit;
    public float critChance = 0;
    public float critDamage = 1;

    //Pollen multiplier increases the amount of pollen per pickup;
    public float pollenMultiplier = 1;
    
    //cdr reduces cooldown between weapon strikes, 1cdr = 1% reduction;
    public float cooldownReduction = 0;
}
