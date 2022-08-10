using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private string leftKey = "a";
    private string rightKey = "d";
    private string upKey = "w";
    private string downKey = "s";
    private string dashKey = "left shift";

    public Rigidbody2D player;

    public float maxSpeed = 7;
    public float acceleration = 30;
    public float dashDuration = 5;
    public float dashSpeed = 10;
    private float xspeed = 0;
    private float yspeed = 0;

    private bool isDahing = false;
    void Start()
    {

    }

    void Update()
    {
        if(!isDahing)
        {
            Movement();
        }
        if(Input.GetKeyDown(dashKey))
        {
            dash();
        }
        MovementDirection();
    }

    void Movement()
    {
            player.velocity = new Vector2(yspeed, player.velocity.y);
            player.velocity = new Vector2(xspeed, player.velocity.x);
    }
    
    void MovementDirection()
    {
        if (Input.GetKey(upKey) | Input.GetKey(downKey))
        {
            if (Input.GetKey(upKey) & Input.GetKey(downKey))
            {
                if (yspeed > 0)
                {
                    yspeed -= acceleration * Time.deltaTime;
                }
                if (yspeed < 0)
                {
                    yspeed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKey(upKey))
            {
                if (yspeed < maxSpeed)
                {
                    yspeed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKey(downKey))
            {
                if (yspeed > -maxSpeed)
                {
                    yspeed -= acceleration * Time.deltaTime;
                }
            }
        }
        else
        {
            if (yspeed > 0)
            {
                yspeed -= acceleration * Time.deltaTime;
                if (yspeed < 0)
                {
                    yspeed = 0;
                }
            }
            if (yspeed < 0)
            {
                yspeed += acceleration * Time.deltaTime;
                if (yspeed > 0)
                {
                    yspeed = 0;
                }
            }
        }

        if (Input.GetKey(leftKey) | Input.GetKey(rightKey))
        {
            if (Input.GetKey(leftKey) & Input.GetKey(rightKey))
            {
                if (xspeed > 0)
                {
                    xspeed -= acceleration * Time.deltaTime;
                }
                if (xspeed < 0)
                {
                    xspeed += acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKey(leftKey))
            {
                if (xspeed > -maxSpeed)
                {
                    xspeed -= acceleration * Time.deltaTime;
                }
            }
            else if (Input.GetKey(rightKey))
            {
                if (xspeed < maxSpeed)
                {
                    xspeed += acceleration * Time.deltaTime;
                }
            }
        }
        else
        {
            if (xspeed > 0)
            {
                xspeed -= acceleration * Time.deltaTime;
                if (xspeed < 0)
                {
                    xspeed = 0;
                }
            }
            if (xspeed < 0)
            {
                xspeed += acceleration * Time.deltaTime;
                if (xspeed > 0)
                {
                    xspeed = 0;
                }
            }
        }
    }

    void dash()
    {
        isDahing = true;
        if(Input.GetKey(rightKey))
        {
            for(int i = 0; i < dashDuration; i++)
            {
                xspeed = dashSpeed;
            }
            xspeed = 0;
            isDahing = false;
        }
    }
}
