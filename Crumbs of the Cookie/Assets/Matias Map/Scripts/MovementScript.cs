using System.Xml.Schema;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class MovementScript : MonoBehaviour
{

    //Configurable parameters
    [Header("Player Movement Stats")]
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpingPower = 20f;
    

    [Header("Ray Casting Utilities")]
    [SerializeField] float castDistance;
    [SerializeField] float waterCastDistance;
    [SerializeField] Vector2 boxSize;
    [SerializeField] Vector2 waterBoxSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask hazardLayer;
    [SerializeField] LayerMask waterLayer;

    [Header("Movement Smoothness")]
    [SerializeField] float coyoteTimer = 0.075f;
    [SerializeField] float jumpBuffering = 0.2f;

    [Header("Gravity Scale")]
    [SerializeField] float gravityScale;
    [SerializeField] float gravityStrength;
    [SerializeField] float fallGravityMult;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fastFallGravityMult;
    [SerializeField] float maxFastFallSpeed;

    //private variables 
    private Vector2 movementInput;
    private Vector2 jumpingInput;
    private float coyoteCounter;
    private float jumpBufferingTimer;
    private float lastGroundedTime = -0.09f;
    private bool isJumping;
    private bool isGrounded;

    //Cached references
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

    private void FixedUpdate()
    {
        Movement();
        SpriteFlip();
        CoyoteJump();
        ExtraGravity();

        if(isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    void OnMove(InputValue inputValue)
    { 
        movementInput = inputValue.Get<Vector2>();
    } 

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (Time.time -lastGroundedTime <= coyoteCounter)
            {
               JumpForce();
            }
        }
    }

    void Movement()
    {
        rb_Player.linearVelocityX = movementInput.x * movementSpeed;
    }

    void JumpForce()
    {
        rb_Player.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
        coyoteCounter = 0f;
    }

    bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    void CoyoteJump()
    {
        if(IsGrounded())
        {
            coyoteCounter = coyoteTimer;
        }
        else
        {
            coyoteCounter -= Time.deltaTime;
        }
    }


    void ExtraGravity()
    {
        if (rb_Player.linearVelocityY > 0)
        {
            SetGravityScale(gravityScale * fallGravityMult);
            rb_Player.linearVelocity = new Vector2(rb_Player.linearVelocity.x,Mathf.Max(rb_Player.linearVelocity.y, -maxFallSpeed));
        }
        

        else
        {
            SetGravityScale(gravityScale);
        }
    }

    public void SetGravityScale(float scale)
    {
        rb_Player.gravityScale = scale;
    }


    void SpriteFlip()
    {
        if (rb_Player.linearVelocityX < 0f)
        {
            spriteRenderer.flipX = true;
        }

        if (rb_Player.linearVelocityX > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
}
