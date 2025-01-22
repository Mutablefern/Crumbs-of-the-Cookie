using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy stats")] 
    [SerializeField] float moveSpeed = 1f;

    [Header("Ground & Ledge Check")]
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] float groundCheck = 1f;
    [SerializeField] LayerMask groundLayer;

    bool isFacingRight;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent < Rigidbody2D>();
    }

    void Move()
    {
        if (CheckGrounded())
        {
            rb.linearVelocityX = transform.right.x * moveSpeed;
        }
        else
        {
            rb.linearVelocityX = 0f;
        }
    }

    void LedgeCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            ledgeCheckPosition.position,
            Vector2.down,
            ledgeCheckLength,
            groundLayer);

        if (hit.collider == null && CheckGrounded())
        {
            isFacingRight = !isFacingRight;

            if (isFacingRight)
            {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, -180f, 0f);
            }
        }
    }

    bool CheckGrounded()
    {
        Collider2D isGrounded = Physics2D.OverlapCircle(transform.position, groundCheck, groundLayer);

        return isGrounded;
    }
}
