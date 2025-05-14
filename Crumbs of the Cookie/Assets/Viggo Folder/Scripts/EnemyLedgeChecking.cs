using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyLedgeChecking : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckOffset;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] float groundCheck = 1f;
    [SerializeField] int knockback;
    [SerializeField] int knockbackY;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Animator enemyAnim;
    [SerializeField] float sightRange;
    [SerializeField] int jumpForce;
    [SerializeField] bool isFacingRight;

    public int enemyState;
    Rigidbody2D rb_Enemy;
    public Transform playerTransform;
    EnemyHealth enemyHealth;
    Vector3 direction;
    bool shouldJump;

    void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
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
                // used for knockback whenever the enemy gets hit
                break;

            case 3:
                break;
        }
    }

    public void Knockback()
    {
        rb_Enemy.linearVelocityY = 0;
        direction = transform.position - enemyHealth.sourceOfKnockback;
        rb_Enemy.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        rb_Enemy.AddForce(transform.up * knockbackY, ForceMode2D.Impulse);
    }
    void Move()
    {
        DetectPlayer();
        JumpAttack();

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
        //Checking if there is a ledge infrot of the enemy
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(ledgeCheckPosition.position.x + ledgeCheckOffset * transform.right.x, ledgeCheckPosition.position.y),
            Vector2.down,
            ledgeCheckLength,
            groundLayer);

        if (hit.collider == null && CheckGrounded())
        {
            Flip();
        }
    }
    void DetectPlayer()
    {
        //is player within sight range?
        if (Vector2.Distance(transform.position, playerTransform.position) <= sightRange)
        {           
            StartCoroutine(AttackWindow());
        }
    }

    IEnumerator AttackWindow()
    {
        shouldJump = true;
        yield return new WaitForSeconds(0.5f);
        shouldJump = false;
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

    void JumpAttack()
    {
        if (CheckGrounded() && shouldJump)
        {
            StartCoroutine(AttackCor());
           
        }
    }
    
    IEnumerator AttackCor()
    {
        
        enemyState = 3;
        enemyAnim.SetTrigger("RatAttack");
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        Vector2 jumpDirection = direction * jumpForce;
        rb_Enemy.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        enemyState = 1; 
    }
}
