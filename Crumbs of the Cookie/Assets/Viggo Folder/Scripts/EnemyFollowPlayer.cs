using Unity.VisualScripting;
using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public float chaseSpeed = 2f;
    public float jumpForce = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb_Enemy;
    private bool isGrounded;
    private bool shouldJump;
   
    void Start()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Grounded
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        //Player direction
        float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
        //Player above detection
        bool isPlayerAbove = Physics2D.Raycast(transform.position, Vector2.up, 5f, 1 << playerTransform.gameObject.layer);

        if (isGrounded)
        {
            //Chase player
            //Velocities
            rb_Enemy.linearVelocity = new Vector2(direction * chaseSpeed, rb_Enemy.linearVelocity.y);

            //Jump if gap
            //Else if player above and platform above

            //If Ground
            RaycastHit2D groundInFront = Physics2D.Raycast(transform.position, new Vector2(direction, 0), 2f, groundLayer);
            //If Gap
            RaycastHit2D gapAhead = Physics2D.Raycast(transform.position + new Vector3(direction, 0, 0), Vector2.down, 2f, groundLayer);
            //If player above
            RaycastHit2D platformAbove = Physics2D.Raycast(transform.position, Vector2.up, 3f, groundLayer);

            if(!groundInFront.collider && !gapAhead.collider)
            {
                shouldJump = true;
            }
            else if(isPlayerAbove && platformAbove.collider)
            {
                shouldJump = true;
            }

        }
    }

    void FixedUpdate()
    {
        if(isGrounded && shouldJump)
        {
            shouldJump = false;
            Vector2 direction = (playerTransform.position - transform.position).normalized;

            Vector2 jumpDirection = direction * jumpForce;

            rb_Enemy.AddForce(new Vector2(jumpDirection.x, jumpForce), ForceMode2D.Impulse);
        }
    }
}
