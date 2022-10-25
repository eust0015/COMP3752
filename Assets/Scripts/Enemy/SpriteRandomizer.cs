using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using Random = System.Random;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField]
    private List<AnimatorController> _controllers;

    private Animator _a;
    private void Awake()
    {
        _a = GetComponent<Animator>();

        Random r = new Random();
        int ran = r.Next(_controllers.Count);
        _a.runtimeAnimatorController = _controllers[ran];
    }
}
