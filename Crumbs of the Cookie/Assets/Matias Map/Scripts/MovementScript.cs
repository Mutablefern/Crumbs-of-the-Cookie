using System.Xml.Schema;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    //Configurable parameters
    [Header("Movement")]
    public float moveSpeed = 10f;
    public float jumpStrength = 10f;
    public float castDistance;
    public LayerMask groundLayer;
    public Vector2 boxSize;

    Vector2 movementVector;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            if (IsGrounded())
            {
                JumpForce();
            }
        }
    }

    void OnMove(InputValue value)
    {
        movementVector = value.Get<Vector2>();
    }

    void Move()
    {
        rb.linearVelocityX = movementVector.x * moveSpeed;

    }

    void JumpForce()
    {
        rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
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
}
