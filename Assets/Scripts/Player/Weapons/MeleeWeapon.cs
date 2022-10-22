using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MeleeWeapon", menuName = "Melee Weapon")]
public class MeleeWeapon : Weapon
{
    public List<Hitbox> Data = new List<Hitbox>();
}
