using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class Hitbox
{
    public float dimX;
    public float dimY;
    public Vector2 relativePos;
    
    public float activeFrames; 
    public int damage;
    public float knockback;
    public float momentum;


    public Hitbox(float _dimX, float _dimY, float _activeFrames, int _damage, Vector2 _relativePos,
        float _knockback = 0, float _momentum = 0)
    {
        dimX = _dimX;
        dimY = _dimY;
        activeFrames = _activeFrames;
        damage = _damage;
        relativePos = _relativePos;
        knockback = _knockback;
        momentum = _momentum;
    }
}
