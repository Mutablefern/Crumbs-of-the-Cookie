using UnityEngine;

public class EnemyLedgeCheckingRollyPolly : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
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
    [SerializeField] GameObject armor;
    Vector3 direction;
    public bool playerDetected;
    bool isFacingRight;
    Rigidbody2D rb_Enemy;
    public bool hasTurned;
    public bool groundDetected;
    public Transform groundPos;
    public float groundCheckSize;
    public bool wallDetected;
    public Transform wallPos;
    public float wallCheckSize;
    private float ZAxisAdd;
    public float fallTime;



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
        RollMode();
        EnvironmentDetection();
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
        rb_Enemy.linearVelocity = transform.right * moveSpeed;
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

    void EnvironmentDetection()
    {
        groundDetected = Physics2D.Raycast(groundPos.position, -transform.up, groundCheckSize, groundLayer);
        wallDetected = Physics2D.Raycast(wallPos.position, transform.right, wallCheckSize, groundLayer);

        if (!groundDetected)
        {
            if (hasTurned == false)
            {
                ZAxisAdd -= 90;
                transform.eulerAngles = new Vector3(0f, 0f, ZAxisAdd);
                hasTurned = true;
            }

            fallTime -= Time.deltaTime;
        }

        if (groundDetected)
        {
            hasTurned = false;
            fallTime = 1f;
        }

        if (wallDetected)
        {
            if (!hasTurned)
            {
                ZAxisAdd += 90;

                transform.eulerAngles = new Vector3(0f, 0f, ZAxisAdd);
            }
        }

        if (fallTime == 1)
        {
            rb_Enemy.gravityScale = 0f;

            minMoveSpeed = 1f;
            maxMoveSpeed = 5f;
        }

        else if (fallTime <= 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            ZAxisAdd = 0f;
            rb_Enemy.gravityScale = 50f;
            minMoveSpeed = 0f;
            maxMoveSpeed = 0f;
        }

        if (ZAxisAdd <= -360f)
        {
            ZAxisAdd = 0f;
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
        if (collision.gameObject.CompareTag("Enemy"))
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
            transform.eulerAngles = new Vector3(0f, 0f, -180f);
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, groundCheck);
        Gizmos.DrawLine(groundPos.position, new Vector2(groundPos.position.x, groundPos.position.y - groundCheckSize));
        Gizmos.DrawLine(wallPos.position, new Vector2(wallPos.position.x + wallCheckSize, wallPos.position.y));
    }
    public void Ondeath()
    {
        Instantiate(armor, transform.position, Quaternion.identity);
    }

}