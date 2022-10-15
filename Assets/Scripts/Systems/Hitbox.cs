using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hitbox
{
    public float dimX;
    public float dimY;
    public float duration; 
    public int damage;
    public Vector2 relativePos;

    public Hitbox(float _dimX, float _dimY, float _duration, int _damage, Vector2 _relativePos)
    {
        dimX = _dimX;
        dimY = _dimY;
        duration = _duration;
        damage = _damage;
        relativePos = _relativePos;
    }
}
