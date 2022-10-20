using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private string leftKey = "Left";
    private string rightKey = "Right";
    private string upKey = "Up";
    private string downKey = "Down";
    private string dashKey = "Fire3";
    private string attackKey = "Fire1";

    public Rigidbody2D player;

    public float maxSpeed = 7;
    public float acceleration = 30;
    public float dashCooldown = 5;
    public float dashSpeed = 10;
    private float xspeed = 0;
    private float yspeed = 0;
    private float dashTimer;

    [SerializeField]
    private float attackCooldown = 2f;
    private float currentAttackCooldown = 0f;
    private Vector2 lastSpeed;

    private bool isDahing = false;

    public Vector2 momentum => new Vector2(xspeed, yspeed);

    private SpriteRenderer _s;
    private AttackController _a;
    private playerAnimation _anim;
    private Transform slash;

    void Start()
    {
        _s = transform.GetChild(0).GetComponent<SpriteRenderer>();
        _a = GetComponent<AttackController>();
        _anim = transform.GetChild(0).GetComponent<playerAnimation>();
        slash = transform.GetChild(1);
        dashTimer = dashCooldown;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        //update the players dash cooldown
        DashCooldown();
        //checks if the player can dash. If they can, it checks if the player wants to dash
        if(Input.GetButton(dashKey) & !isDahing)
        {
            Dash();
        }
        //figures out what direction the player should be moving
        MovementDirection();
        //moves the player in that direction
        Movement();
        currentAttackCooldown -= Time.deltaTime;
        Attack();
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
        if (Input.GetButton(upKey) | Input.GetButton(downKey) & Mathf.Abs(yspeed) <= maxSpeed)
        {
            //slows the player down, if they are holding up and down at the same time
            if (Input.GetButton(upKey) & Input.GetButton(downKey))
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
            else if (Input.GetButton(upKey))
            {
                if (yspeed < maxSpeed)
                {
                    lastSpeed = Vector2.up;
                    yspeed += acceleration * Time.deltaTime;
                }
            }
            //speeds the player up in the down direction and makes sure they don't go over the max speed
            else if (Input.GetButton(downKey))
            {
                if (yspeed > -maxSpeed)
                {
                    lastSpeed = Vector2.down;
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
        if (Input.GetButton(leftKey) | Input.GetButton(rightKey) & Mathf.Abs(xspeed) <= maxSpeed)
        {
            //slows the player down, if they are holding left and right at the same time
            if (Input.GetButton(leftKey) & Input.GetButton(rightKey))
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
            else if (Input.GetButton(leftKey))
            {
                if (xspeed > -maxSpeed)
                {
                    _s.flipX = true;
                    lastSpeed = Vector2.left;
                    xspeed -= acceleration * Time.deltaTime;
                }
            }
            //speeds the player up in the right direction and makes sure they don't go over the max speed
            else if (Input.GetButton(rightKey))
            {
                if (xspeed < maxSpeed)
                {
                    _s.flipX = false;
                    lastSpeed = Vector2.right;
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
        if (Input.GetButton(rightKey))
        {
            isDahing = true;
            xspeed = dashSpeed;
        }
        if (Input.GetButton(leftKey))
        {
            isDahing = true;
            xspeed = -dashSpeed;
        }
        if (Input.GetButton(upKey))
        {
            isDahing = true;
            yspeed = dashSpeed;
        }
        if (Input.GetButton(downKey))
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

    void Attack()
    {
        if (Input.GetButton(attackKey) && lastSpeed != Vector2.zero)
        {
            Hitbox h;
            slash.localPosition = new Vector3(lastSpeed.x, lastSpeed.y, slash.localPosition.z);

            switch (lastSpeed.y)
            {
                case 1 :
                    slash.GetComponent<SpriteRenderer>().flipX = true;
                    slash.localEulerAngles = new Vector3(0, 0, -90);
                    break;
                case -1:
                    slash.GetComponent<SpriteRenderer>().flipX = false;
                    slash.localEulerAngles = new Vector3(0, 0, -90);
                    break;
                default:
                    slash.localEulerAngles = new Vector3(0, 0, 0);
                    break;
            }

            switch (lastSpeed.x)
            {
                case 1:
                    h = new Hitbox(1f, 2f, 0.2f, 3, lastSpeed);
                    slash.GetComponent<SpriteRenderer>().flipX = false;
                    break;
                case -1:
                    h = new Hitbox(1f, 2f, 0.2f, 3, lastSpeed);
                    slash.GetComponent<SpriteRenderer>().flipX = true;
                    break;
                default:
                    h = new Hitbox(2f, 1, 0.2f, 3, lastSpeed);
                    break;
            }

            if (currentAttackCooldown <= 0)
            {
                StartCoroutine(_anim.AttackAnim());
                _a.RequestHitbox(h);
                currentAttackCooldown = attackCooldown;
            }
            
            
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
