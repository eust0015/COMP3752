using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Enemy;
using Player;
using UI.Statistics;
using UnityEngine;

public class Projectile
{
    public float speed;
    public Sprite sprite;
    public int damage;
    public bool pierce;
    public int pierceCount;
    public float duration;

    public Projectile(float _speed, Sprite _sprite, int _damage, float _duration, bool _pierce = false, int _pierceCount = -1)
    {
        speed = _speed;
        sprite = _sprite;
        damage = _damage;
        duration = _duration;
        pierce = _pierce;
        pierceCount = _pierceCount;
    }
}
