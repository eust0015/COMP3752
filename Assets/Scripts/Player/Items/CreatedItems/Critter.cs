using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Critter : PlayerItem
{
    public override void StatModifiers()
    {
        _stats.critChance += 10f;
    }
}
