using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{

    public Transform playerTransform;
    [Tooltip("2 is recommended")]
    public float chaseSpeed = 2f;
    [Tooltip("2 is recommended")]
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    [SerializeField] float castDistance;

    private bool isGrounded;
    private bool shouldJump;
    public GameObject player;
    public float sightRange;
    public bool playerDetected;

    private Rigidbody2D rb_Enemy;

    void Awake()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
        playerDetected = false;
    }

    void Update()
    {
        DetectPlayer();
        Debug.Log(isGrounded);

        if (playerDetected)
        {
            //Grounded
            isGrounded = Physics2D.Raycast(transform.position, Vector2.down, castDistance, groundLayer);
            //Player direction
            float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
            //Player above detection
            bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << playerTransform.gameObject.layer);

            if (isGrounded)
            {
                rb_Enemy.linearVelocity = new Vector2(direction * chaseSpeed, rb_Enemy.linearVelocity.y);

                //If ground
                RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
                //If gap
                RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
                //If player above
                RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

                Debug.DrawRay(transform.position, Vector2.down, Color.red);

                if (!groundInFront.collider && !gapAhead.collider)
                {
                    shouldJump = true;
                }
                else if (isPlayerAbove && platformAbove.collider)
                {
                    shouldJump = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, castDistance, groundLayer);

        if (isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            Vector2 jumpDirection = direction * jumpForce;
            rb_Enemy.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
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
}
