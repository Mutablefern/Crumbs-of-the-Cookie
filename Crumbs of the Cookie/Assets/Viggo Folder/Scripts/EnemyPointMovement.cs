using System.Collections;
using UnityEngine;

public class EnemyPointMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int knockback;
    [SerializeField] int knockbackY;
    public int enemyState;

    public GameObject PointA;
    public GameObject PointB;

    Transform currentPoint;
    Rigidbody2D rb_Enemy;
    EnemyHealth enemyHealth;
    [SerializeField] int gravScale;

    Vector3 direction;


    void Awake()
    {
        enemyState = 1;
        rb_Enemy = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        currentPoint = PointB.transform;
    }

    void Update()
    {
        //Check proximity to switch points
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {

            flip();
            currentPoint = currentPoint == PointB.transform ? PointA.transform : PointB.transform;
        }
    }
    private void FixedUpdate()
    {
        switch (enemyState)
        {
            case 1:

                //Calculate direction and move
                direction = (currentPoint.position - transform.position).normalized;
                rb_Enemy.linearVelocity = direction * moveSpeed;

                break;

            case 2:

                //Enemy knockback
                //rb_Enemy.gravityScale = gravScale;

                break;

            case 3:

                //Enemy knockback
               


                break;

        }


       




    }
    public void Knockback()
    {
        //Add variable colider from enemyHealth
        direction = transform.position - enemyHealth.sourceOfKnockback;
        //direction.y = 0;
        rb_Enemy.AddForce(direction.normalized * knockback, ForceMode2D.Impulse);
        rb_Enemy.AddForce(transform.up * knockbackY, ForceMode2D.Impulse);
    }

    void flip()
    {
        //Flips the game object
        Vector3 localscale = transform.localScale;
        localscale.x *= -1;
        transform.localScale = localscale;
    }

    void OnDrawGizmos()
    {
        if (PointA != null && PointB != null)
        {
            Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
            Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
            Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
        }
    }
}
