using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{ 
    // Configurable parameters
    [SerializeField] Animator playerAnim;
    [SerializeField] Transform runtoTransform;
    [SerializeField] bool startRun;
    [SerializeField] public float timeInAir = 1f;
    Vector3 runtoPosition;

    [Header("Player Movement Stats")]
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpingPower = 20f;
    [SerializeField] float dragForce; 

    [Header("Ray Casting Utilities")]
    [SerializeField] float castDistance;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;

    [Header("Movement Smoothness")]
    [SerializeField] float coyoteTimer = 0.075f;  
    [SerializeField] float jumpBuffering = 0.2f; 

    [Header("Gravity Scale")]
    [SerializeField] float gravityScale;
    [SerializeField] float gravityStrength;
    [SerializeField] float fallGravityMult;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fastFallGravityMult;
    [SerializeField] float minFastFallSpeed;

    // Private variables
    private Vector2 movementInput;
    private float coyoteCounter;
    private float jumpBufferingTimer;
    private float lastGroundedTime = -0.09f;
    [SerializeField] private bool jumpPressed;
    public bool isGrounded;

    // Cached references
    Rigidbody2D rb_Player;

    private void Awake()
    {
        rb_Player = GetComponent<Rigidbody2D>();
        startRun = true;
        runtoPosition = runtoTransform.position;
    }

    private void Start()
    {
        SetGravityScale(gravityScale);
    }

    public void Update()
    {
        if (startRun)
        {
            movementInput.x = 1;
            playerAnim.SetBool("Running", true);
            if (transform.position.x > runtoPosition.x) // it stops even if it overshoots, but not if it undershoots
            {
                startRun = false;
                movementInput.x = 0;
            }
        }
        if (!startRun)
        {
            if (IsGrounded())
            {
                playerAnim.SetBool("IsGrounded", true);
                lastGroundedTime = Time.time;
                coyoteCounter = coyoteTimer;
            }
            else
            {
                playerAnim.SetBool("IsGrounded", false);
                coyoteCounter -= Time.deltaTime;
                jumpBufferingTimer -= Time.deltaTime;
            }

            VariableJumping();
        }
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

    
    void OnMove(InputValue inputValue)
    {
        if (!startRun)
        {
            movementInput = inputValue.Get<Vector2>();
        }
    }

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            jumpPressed = true;
            jumpBufferingTimer = jumpBuffering;  
        }

        else
        {
            Debug.Log("no");
            jumpPressed = false;
        }
    }

    void Movement()
    {
        rb_Player.linearVelocity = new Vector2(movementInput.x * movementSpeed, rb_Player.linearVelocity.y);
        if (rb_Player.linearVelocity.x != 0)
        {
            playerAnim.SetBool("Running", true);
        }
        else
        {
            playerAnim.SetBool("Running", false);
        }
    }

    void JumpForce()
    {
        playerAnim.SetTrigger("Jump");
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

    public bool IsGrounded()
    {
        Debug.DrawRay(transform.position, -transform.up*castDistance, Color.red);
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
    }

    void ExtraGravity()
    {
        if (rb_Player.linearVelocity.y > 0)
        {
            SetGravityScale(gravityScale * fallGravityMult);
            rb_Player.linearVelocity = new Vector2(rb_Player.linearVelocity.x, Mathf.Max(rb_Player.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            SetGravityScale(gravityScale);
        }

        if(rb_Player.linearVelocity.y < 0)
        {
            SetGravityScale(gravityScale * fallGravityMult);
        }
    }


    public void SetGravityScale(float scale)
    {
        rb_Player.gravityScale = scale;
    }

    void SpriteFlip()
    {
        if (rb_Player.linearVelocity.x < 0f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        }

        if (rb_Player.linearVelocity.x > 0f)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
    }
}