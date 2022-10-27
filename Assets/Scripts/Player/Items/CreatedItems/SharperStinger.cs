using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharperStinger : PlayerItem
{
    public override void StatModifiers()
    {
        _stats.atkModifier += 2;
    }
}
