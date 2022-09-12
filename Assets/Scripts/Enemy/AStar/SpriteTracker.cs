using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTracker : MonoBehaviour
{
    private Transform pos;
    private Transform sprite;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.GetChild(0);
        sprite = transform.GetChild(1);
    }
    void FixedUpdate()
    {
        sprite.position = new Vector3(pos.position.x, pos.position.y);
    }
}
