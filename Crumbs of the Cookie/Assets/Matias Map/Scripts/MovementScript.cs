using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    // Configurable parameters
    [Header("Player Movement Stats")]
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpingPower = 20f;

    [Header("Ray Casting Utilities")]
    [SerializeField] float castDistance;
    [SerializeField] Vector2 boxSize;
    [SerializeField] LayerMask groundLayer;

    [Header("Movement Smoothness")]
    [SerializeField] float coyoteTimer = 0.075f;  // Coyote time duration
    [SerializeField] float jumpBuffering = 0.2f; // Jump buffering time

    [Header("Gravity Scale")]
    [SerializeField] float gravityScale;
    [SerializeField] float gravityStrength;
    [SerializeField] float fallGravityMult;
    [SerializeField] float maxFallSpeed;
    [SerializeField] float fastFallGravityMult;
    [SerializeField] float maxFastFallSpeed;

    // Private variables
    private Vector2 movementInput;
    private float coyoteCounter;
    private float jumpBufferingTimer;
    private float lastGroundedTime = -0.09f;
    private bool jumpPressed;
    private bool isGrounded;

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

        
        if (IsGrounded())
        {
            lastGroundedTime = Time.time;
            coyoteCounter = coyoteTimer;  
        }
        else
        {
            coyoteCounter -= Time.deltaTime;  
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

    bool IsGrounded()
    {
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
    }

    public void SetGravityScale(float scale)
    {
        rb_Player.gravityScale = scale;
    }

    void SpriteFlip()
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