using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public GameObject PointA;
    public GameObject PointB;

    Transform currentPoint;
    Rigidbody2D rb_Enemy;

    void Start()
    {
        rb_Enemy = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform;
    }

    void Update()
    {
        // Calculate direction and move
        Vector2 direction = (currentPoint.position - transform.position).normalized;
        rb_Enemy.linearVelocity = direction * moveSpeed;

        // Check proximity to switch points
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            flip();
            currentPoint = currentPoint == PointB.transform ? PointA.transform : PointB.transform;
        }
    }

    private void flip()
    {
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
