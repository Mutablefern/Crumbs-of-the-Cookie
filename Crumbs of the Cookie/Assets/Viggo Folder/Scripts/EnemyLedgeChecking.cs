using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLedgeChecking : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] float groundCheck = 1f;
    [SerializeField] int knockback;
    [SerializeField] int knockbackY;
    [SerializeField] LayerMask groundLayer;
    public int enemyState;
    [SerializeField] Animator enemyAnim;

    [SerializeField] bool isFacingRight;

    Rigidbody2D rb_Enemy;
    EnemyHealth enemyHealth;
    Vector3 direction;
   

    void Awake()
    {
        enemyState = 1;
        rb_Enemy = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {


        LedgeCheck();

        switch (enemyState)
        {
            case 1:

                Move();

                break;

            case 2:

                

                break;

        }
    } 

    public void Knockback()
    {
        direction = transform.position - enemyHealth.sourceOfKnockback;
        rb_Enemy.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        rb_Enemy.AddForce(transform.up * knockbackY, ForceMode2D.Impulse);
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
                Flip();
            }
        }
    


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Flip();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyAnim.SetTrigger("RatAttack");
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

    void Flip()
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
