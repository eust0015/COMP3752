using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Velocity : MonoBehaviour
{
    private Rigidbody2D _rbd2;

    private void Awake()
    {

        _rbd2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_rbd2.velocity.x != 0 || _rbd2.velocity.y != 0)
        {
            _rbd2.velocity = Vector2.Lerp(_rbd2.velocity, Vector2.zero, 4 * Time.deltaTime);
        }
    }
}
