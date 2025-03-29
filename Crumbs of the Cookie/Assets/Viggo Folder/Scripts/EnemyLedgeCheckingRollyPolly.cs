using UnityEngine;

public class EnemyLedgeCheckingRollyPolly : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform ledgeCheckPosition;
    [SerializeField] float ledgeCheckLength;
    [SerializeField] float groundCheck = 1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform playerTransform;
    [SerializeField] float minMoveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] int knockback;
    [SerializeField] int knockbackY;
    public int enemyState;
    public float sightRange;
    Animator animator;
    EnemyHealth enemyHealth;
    Vector3 direction;
    public bool playerDetected;
    bool isFacingRight;

    Rigidbody2D rb_Enemy;

    void Awake()
    {
        enemyState = 1;
        rb_Enemy = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate()
    {

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

    private void Update()
    {
        DetectPlayer();
        LedgeCheck();
        RollMode();
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
            rb_Enemy.linearVelocityX = transform.right.x * moveSpeed;
      
    }

    void LedgeCheck()
    {
        //Checking if there is a ledge infrot of the enemy
        RaycastHit2D hit = Physics2D.Raycast(
            ledgeCheckPosition.position,
            Vector2.down,
            ledgeCheckLength,
            groundLayer);

        if (hit.collider == null)
        {
            Flip();
        }
    }

    

    void DetectPlayer()
    {
        //is player within sight range?
        if (Vector2.Distance(transform.position, playerTransform.position) <= sightRange)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    void RollMode()
    {
        if (playerDetected == true)
        {
            animator.SetBool("isRolling", true);
            moveSpeed = maxMoveSpeed;
        }
        else if (playerDetected == false)
        {
            animator.SetBool("isRolling", false);
            moveSpeed = minMoveSpeed;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Flip();
        }
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


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheck);
    }
}