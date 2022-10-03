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
    public float dashCooldown = 5;
    public float dashSpeed = 10;
    private float xspeed = 0;
    private float yspeed = 0;
    private float dashTimer;

    private bool isDahing = false;

    public Vector2 momentum => new Vector2(xspeed, yspeed);

    private SpriteRenderer _s;

    void Start()
    {
        _s = transform.GetChild(0).GetComponent<SpriteRenderer>();
        dashTimer = dashCooldown;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        //update the players dash cooldown
        DashCooldown();
        //checks if the player can dash. If they can, it checks if the player wants to dash
        if(Input.GetKey(dashKey) & !isDahing)
        {
            Dash();
        }
        //figures out what direction the player should be moving
        MovementDirection();
        //moves the player in that direction
        Movement();
    }

    void Movement()
    {
        //Moves the player in the provided direction    
        player.velocity = new Vector2(yspeed, player.velocity.y);
        player.velocity = new Vector2(xspeed, player.velocity.x);
    }
    
    void MovementDirection()
    {
        //Checks if the player is moving up or down and checks if the player isn't at their max speed
        if (Input.GetKey(upKey) | Input.GetKey(downKey) & Mathf.Abs(yspeed) <= maxSpeed)
        {
            //slows the player down, if they are holding up and down at the same time
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
            //speeds the player up in the up direction and makes sure they don't go over the max speed
            else if (Input.GetKey(upKey))
            {
                if (yspeed < maxSpeed)
                {
                    yspeed += acceleration * Time.deltaTime;
                }
            }
            //speeds the player up in the down direction and makes sure they don't go over the max speed
            else if (Input.GetKey(downKey))
            {
                if (yspeed > -maxSpeed)
                {
                    yspeed -= acceleration * Time.deltaTime;
                }
            }
        }
        //if the player isn't holding up or down, it slows the players velocity on the y axis
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

        //Checks if the player is moving left or right and checks if the player isn't at their max speed
        if (Input.GetKey(leftKey) | Input.GetKey(rightKey) & Mathf.Abs(xspeed) <= maxSpeed)
        {
            //slows the player down, if they are holding left and right at the same time
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
            //speeds the player up in the left direction and makes sure they don't go over the max speed
            else if (Input.GetKey(leftKey))
            {
                if (xspeed > -maxSpeed)
                {
                    _s.flipX = true;
                    xspeed -= acceleration * Time.deltaTime;
                }
            }
            //speeds the player up in the right direction and makes sure they don't go over the max speed
            else if (Input.GetKey(rightKey))
            {
                if (xspeed < maxSpeed)
                {
                    _s.flipX = false;
                    xspeed += acceleration * Time.deltaTime;
                }
            }
        }
        //if the player isn't holding left or right, it slows the players velocity on the x axis
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

    void Dash()
    {
        //Figures out what direction the player is facing and adds momentum to that direction and sets dashing to true
        if (Input.GetKey(rightKey))
        {
            isDahing = true;
            xspeed = dashSpeed;
        }
        if (Input.GetKey(leftKey))
        {
            isDahing = true;
            xspeed = -dashSpeed;
        }
        if (Input.GetKey(upKey))
        {
            isDahing = true;
            yspeed = dashSpeed;
        }
        if (Input.GetKey(downKey))
        {
            isDahing = true;
            yspeed = -dashSpeed;
        }
    }

    void DashCooldown()
    {
        //reduces the dashing timer as time passes
        if(isDahing)
        {
            dashTimer -= Time.deltaTime;
        }
        //when the timer runs down, resets the cooldown
        if (dashTimer <= 0)
        {
            isDahing = false;
            dashTimer = dashCooldown;
        }
    }

    public void TeleportPlayer(string direction)
    {
        switch (direction)
        {
            case "Above":
                gameObject.transform.position = new Vector3(0, (gameObject.transform.position.y-2)*-1, -5);
                break;
            case "Below":
                gameObject.transform.position = new Vector3(0, (gameObject.transform.position.y+2)*-1, -5);
                break;
            case "Left":
                gameObject.transform.position = new Vector3((gameObject.transform.position.x+1)*-1, 0, -5);
                break;
            case "Right":
                gameObject.transform.position = new Vector3((gameObject.transform.position.x-1)*-1, 0, -5);
                break;
            default:
                break;
        }
        xspeed = 0;
        yspeed = 0;
    }
}
