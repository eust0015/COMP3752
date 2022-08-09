using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D player;
    public float moveSpeed = 5;
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("W"))
        {
            player.velocity = new Vector2(moveSpeed, player.velocity.y);
        }
    }
}
