using System.Xml.Schema;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    //Configurable parameters
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpingPower = 20f;
    [SerializeField] float castDistance;
    [SerializeField] float waterCastDistance;
    [SerializeField] Vector2 boxSize;
    [SerializeField] Vector2 waterBoxSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask hazardLayer;
    [SerializeField] LayerMask waterLayer;
    [SerializeField] float coyoteTimer = 0.075f;
    [SerializeField] float jumpBuffering = 0.2f;

    //private variables 
    private Vector2 movementInput;
    private Vector2 jumpingInput;
    private bool isGrounded;
    private float coyoteCounter;
    private float jumpBufferingTimer;

    //Cached references
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Movement();
        SpriteFlip();
        CoyoteJump();
    }

    void OnMove(InputValue inputValue)
    { 
        movementInput = inputValue.Get<Vector2>();
    } 

    void OnJump(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            if (coyoteCounter > 0f)
            {
               JumpForce();

               
            }
        }
    }

    void Movement()
    {
        rb.linearVelocityX = movementInput.x * movementSpeed;
    }

    void JumpForce()
    {
        rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
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

    void SpriteFlip()
    {
        if (rb.linearVelocityX < 0f)
        {
            spriteRenderer.flipX = true;
        }

        if (rb.linearVelocityX > 0f)
        {
            spriteRenderer.flipX = false;
        }
    }
}
