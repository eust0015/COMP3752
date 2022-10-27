using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PassiveItem : PlayerItem
{
    private AttackController _a;
    private void Awake()
    {
        StartCoroutine(Effect());
        StatModifiers();
        _a = GetComponent<AttackController>();
        _a.onHit += OnHit;
        _a.onHurt += OnHurt;
        _a.onKill += OnKill;
    }

    private IEnumerator Effect()
    {
        yield return null;
    }

    private void OnHit()
    {
        Debug.Log("hit");
    }

    private void OnHurt()
    {
        Debug.Log("hurt");
    }

    private void OnKill()
    {
        Debug.Log("Kill");
    }
}
