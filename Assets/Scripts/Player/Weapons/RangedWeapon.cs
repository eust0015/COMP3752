using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New RangedWeapon", menuName = "Ranged Weapon")]
public class RangedWeapon : Weapon
{
    public int projectileCount;
    public List<Projectile> projectiles = new List<Projectile>();

    public float projDelay;
}
