using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLedgeChecking : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] float groundCheck = 1f;
    [SerializeField] LayerMask groundLayer;

    bool isFacingRight;

    Rigidbody2D rb_Enemy;

    void Awake()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        LedgeCheck();
    }

    void Move()
    {
        if (CheckGrounded())
        {
            rb_Enemy.linearVelocityX = transform.right.x * moveSpeed;
        }
        else
        {
            rb_Enemy.linearVelocityX = 0f;
        }
    }

    void LedgeCheck()
    {
        //Checking if ledge infront
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheck);
    }
}
