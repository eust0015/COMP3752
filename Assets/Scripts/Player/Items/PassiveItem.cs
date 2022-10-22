using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassiveItem : PlayerItem
{
    private void Awake()
    {
        StartCoroutine(Effect());
        StatModifiers();
    }

    public IEnumerator Effect()
    {
        yield return null;
    }

    public void OnHit()
    {
        
    }

    public void OnHurt()
    {
        
    }

    public void OnKill()
    {
        
    }
}
