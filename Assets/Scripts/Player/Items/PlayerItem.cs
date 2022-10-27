using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public string itemName;
    public Sprite itemImage;

    protected PlayerStats _stats;
    protected PlayerHealth _health;

    private void Awake()
    {
        _stats = this.GetComponent<PlayerStats>();
    }

    public virtual void StatModifiers()
    {
        //Modify Stats
    }
}
