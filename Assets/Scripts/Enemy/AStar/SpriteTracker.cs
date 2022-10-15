using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpriteTracker : MonoBehaviour
{
    private Transform pos;
    private Transform sprite;

    [SerializeField] private bool keepRotation;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.GetChild(0);
        sprite = transform.GetChild(1);
    }
    void FixedUpdate()
    {
        sprite.position = new Vector3(pos.position.x, pos.position.y);
        if (keepRotation)
        {
            sprite.transform.eulerAngles = pos.transform.eulerAngles;
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().flipX = pos.transform.rotation.z <= 0;
        }
    }
}
