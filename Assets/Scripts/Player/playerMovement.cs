using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private string leftKey = "a";
    private string rightKey = "d";
    private string upKey = "w";
    private string downKey = "s";
    public Rigidbody2D player;
    public float moveSpeed = 5;
    private float directionY = 0;
    private float directionX = 0;
    void Start()
    {

    }

    void Update()
    {
        MovementDirection();
        player.velocity = new Vector2(directionY * moveSpeed, player.velocity.y);
        player.velocity = new Vector2(directionX * moveSpeed, player.velocity.x);
    }

    void MovementDirection()
    {
        if (Input.GetKeyDown(upKey))
        {
            directionY = 1;
        }
        if (Input.GetKeyDown(downKey))
        {
            directionY = -1;
        }
        if (Input.GetKeyDown(leftKey))
        {
            directionX = -1;
        }
        if (Input.GetKeyDown(rightKey))
        {
            directionX = 1;
        }
        if (Input.GetKeyUp(upKey) || Input.GetKeyUp(downKey))
        {
            directionY = 0;
        }
        if (Input.GetKeyUp(leftKey) || Input.GetKeyUp(rightKey))
        {
            directionX = 0;
        }
    }
}
