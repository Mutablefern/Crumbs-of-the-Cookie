using UnityEngine;

public class MovingPlatform: MonoBehaviour
{
    public float speed;
    public Transform pointA;
    public Transform pointB;

    private Vector3 nextPosition;
    Rigidbody2D rb_MovingPlatform;

    void Start()
    {
        nextPosition = pointB.position;
        rb_MovingPlatform = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}