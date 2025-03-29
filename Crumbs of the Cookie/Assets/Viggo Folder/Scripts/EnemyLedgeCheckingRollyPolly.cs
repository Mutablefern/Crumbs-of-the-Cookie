using UnityEngine;

public class EnemyLedgeCheckingRollyPolly : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
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
    public float detectionDistance = 0.2f;

    private Vector2 moveDirection = Vector2.right;
    private Vector2 currentNormal = Vector2.up; 
    Rigidbody2D rb_Enemy;

    void Awake()
    {
        enemyState = 1;
        rb_Enemy = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        rb_Enemy.gravityScale = 0;
        rb_Enemy.freezeRotation = true;
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
        DetectSurface();
        DetectPlayer();
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
        rb_Enemy.linearVelocity = moveDirection * moveSpeed;
    }

    void DetectSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, detectionDistance, groundLayer);
        Debug.DrawRay(transform.position, moveDirection * detectionDistance, Color.red);
        if(hit)
        {
          AlignToSurface(hit.normal);
        }
        else
        {
            RaycastHit2D downhit = Physics2D.Raycast(transform.position, -currentNormal, detectionDistance, groundLayer);
            Debug.DrawRay(transform.position, -currentNormal * detectionDistance, Color.blue);
            if (downhit)
            {
                AlignToSurface(downhit.normal);
            }
        }
    }

      void AlignToSurface(Vector2 newNormal)
      {
        currentNormal = newNormal;

        moveDirection = new Vector2(-currentNormal.y, currentNormal.x);

        float angle = Mathf.Atan2(currentNormal.y, currentNormal.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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
}