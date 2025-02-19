using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    // Configurable parameters
    [Header("Player Movement Stats")]
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpingPower = 20f;
    [SerializeField] float dragForce; 

    [Header("Ray Casting Utilities")] // Neccesary variables for raycasting. 
    [SerializeField] float castDistance;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;

    [Header("Movement Smoothness")]
    [SerializeField] float coyoteTimer = 0.075f;  
    [SerializeField] float jumpBuffering = 0.2f; 

    [Header("Gravity Scale")] // variables which are needed for extra gravity. 
    [SerializeField] float gravityScale;
    [SerializeField] float gravityStrength;
    [SerializeField] float fallGravityMult;
    [SerializeField] float maxFallSpeed;

    // Private variables
    private Vector2 movementInput;
    private float coyoteCounter; // Same as jumpbufferingtimer. 
    private float jumpBufferingTimer; // The variable we use to make the jumpbuffering decay over time. A "Timer". 
    private bool jumpPressed;
    

    // Cached references
    Rigidbody2D rb_Player;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        SetGravityScale(gravityScale);
    }

    private void Update()
    {
        if (IsGrounded())
        {
            coyoteCounter = coyoteTimer;
        }
        else // Make the different timers decay over time. 
        {
            coyoteCounter -= Time.deltaTime;
            jumpBufferingTimer -= Time.deltaTime;
        }

        VariableJumping(); 

        Debug.DrawRay(transform.position, Vector3.down, Color.yellow);
    }

    private void FixedUpdate()
    {
        Movement();
        SpriteFlip();
        ExtraGravity();
       
        if (jumpPressed && jumpBufferingTimer > 0)
        {
            if (IsGrounded() || coyoteCounter > 0)  
            {
                JumpForce();
                jumpBufferingTimer = 0;  
            }
        }  
    }

    
    void OnMove(InputValue inputValue) //Used to we actually let unity know that we have input from player. 
    {
        movementInput = inputValue.Get<Vector2>(); // our inputValue we get from the player ("wasd") becomes movementInput.
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed) // Unity detectes spacebar being pressed. 
        {
            jumpPressed = true;
            jumpBufferingTimer = jumpBuffering;  
        }

        else
        {
            jumpPressed = false;
        }
    }

    void Movement()
    {
        rb_Player.linearVelocity = new Vector2(movementInput.x * movementSpeed, rb_Player.linearVelocity.y);
    }

    void JumpForce()
    {
        rb_Player.linearVelocity = new Vector2(rb_Player.linearVelocity.x, jumpingPower);  
        coyoteCounter = 0f; 
    }

    void VariableJumping()
    {
        if (rb_Player.linearVelocityY > 0 && !Input.GetButton("Jump"))
        {
            rb_Player.AddForce(-transform.up * dragForce);
        }
    }

    bool IsGrounded() // Raycasting, for ground check. 
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    void ExtraGravity()
    {
        if (rb_Player.linearVelocity.y > 0) //If rb velocity.y is bigger then 0, Adds extra gravity. 
        {
            SetGravityScale(gravityScale * fallGravityMult);
            rb_Player.linearVelocity = new Vector2(rb_Player.linearVelocity.x, Mathf.Max(rb_Player.linearVelocity.y, -maxFallSpeed));
        }
        else // If rb velocity.y isnt bigger than 0 set gravity to normal. 
        {
            SetGravityScale(gravityScale);
        }

        if(rb_Player.linearVelocity.y < 0)
        {
            SetGravityScale(gravityScale * fallGravityMult);
        }
    }

    public void SetGravityScale(float scale) // For extra gravity. 
    {
        rb_Player.gravityScale = scale;
    }

    void SpriteFlip() //Makes the sprite flip, by changing the value of the sprite render to true/false, depending on where the player is moving.
    {
        if (rb_Player.linearVelocity.x < 0f)
        {
            spriteRenderer.flipX = true;
        }

        if (rb_Player.linearVelocity.x > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
}